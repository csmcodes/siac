# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**SIAC** (Sistema Administrativo Contable) is an ASP.NET WebForms ERP system targeting .NET Framework 4.8, targeted at Ecuadorian businesses. It handles accounting, billing, inventory, transport logistics, and SRI (Ecuador tax authority) electronic document generation.

## Build

Build using Visual Studio or MSBuild with the solution file:

```
msbuild SolutionAdministrativa.sln /p:Configuration=Debug
```

Build order matters due to inter-project dependencies — MSBuild resolves this automatically from the `.sln` file. There are no automated tests.

## Architecture

The solution uses a strict **4-layer architecture** with these projects:

```
WebUI / WebReports          ← Presentation (ASP.NET WebForms .aspx pages)
    ↓
BusinessLogicLayer (BLL)    ← Business rules, thin pass-through layer
    ↓
DataAccessLayer (DAL)       ← Provider routing (SQL Server vs PostgreSQL)
    ↓
SqlDataBase / SqlDataBasePG ← Raw ADO.NET execution engines
```

Supporting libraries used throughout:

| Project | Purpose |
|---|---|
| `BusinessObjects` | Plain C# entity classes with `[Data]` attributes for ORM mapping |
| `Services` | `Constantes`, `Dictionaries`, `HtmlElements` — shared helpers for UI construction |
| `Functions` | Utility methods (`Conversiones`, `Formatos`, `Validaciones`) |
| `HtmlElements` | Custom HTML builder objects (`Input`, `Select`, `Textarea`, `Tab`, etc.) used server-side to emit HTML strings |
| `HtmlObjectsMetro` | Metro-style variant of `HtmlElements` |
| `Packages` | Wrappers for external integrations: SRI electronic documents, banking (BAN), reporting |
| `ExceptionHandling` | Shared exception utilities |
| `WebReports` | Separate web app for RDLC/ReportViewer report rendering |

## Database Provider

The active database provider is configured in `WebUI/Web.config` via `appSettings`:

```xml
<add key="provider" value="PostgreSQL" />   <!-- or SqlServer -->
<add key="connection" value="Server=...;Database=...;User ID=...;Password=...;" />
```

`DAL.GetProvider()` reads this at runtime and routes every call to either `SqlDataBase.DB` (SQL Server) or `SqlDataBasePG.DB` (PostgreSQL). Both DB classes share the same API surface. Always implement both branches when writing DAL methods.

## ORM Pattern

There is no Entity Framework. The custom ORM uses **reflection on `[Data]` attributes** on `BusinessObjects` entity classes:

- `[Data(key = true, auto = true)]` — primary key, auto-increment (excluded from INSERT)
- `[Data(originalkey = true)]` — holds the pre-update value of a key (used in UPDATE WHERE clause)
- `[Data(noupdate = true)]` — excluded from UPDATE
- `[Data(noprop = true)]` — excluded from all SQL generation
- Properties without `[Data]` are included in INSERT/UPDATE but excluded from DELETE/GetByPK

`Sentences.GetSentence(SentenceType, properties, tablename)` auto-generates SQL from the reflected property list. `WhereParams` is used for parameterized WHERE clauses using positional `{0}`, `{1}` placeholders.

## Adding a New Entity (typical pattern)

1. **BusinessObjects** — create `MyEntity.cs` with `[Data]` attributes, inherit nothing
2. **DataAccessLayer** — create `MyEntityDAL.cs` with static methods calling `SqlDataBase.DB.*SQL(...)` and `SqlDataBasePG.DB.*SQL(...)` for both providers
3. **BusinessLogicLayer** — create `MyEntityBLL.cs` delegating to `MyEntityDAL`
4. **WebUI** — create `wfMyEntity.aspx` + `.aspx.cs`; build the form using `HtmlElements` objects (e.g., `new Input{...}.ToString()`) and expose `[WebMethod]` static methods for AJAX calls from the page's JS

## Web Services

AJAX backend is in `WebUI/ws/Metodos.asmx` and `Metodos2.asmx`. These are `[ScriptService]`-decorated ASMX web services. WebMethod responses are HTML strings or JSON serialized via `JavaScriptSerializer`. The `ws/` path is exempt from authentication in `Web.config`.

## UI Conventions

- All `.aspx` pages use code-behind (`.aspx.cs`) with `partial class` pattern
- Forms are built by emitting HTML strings server-side using `HtmlElements` classes (`Input`, `Select`, `Textarea`, `Tab`, `Tabs`, `ListItem`, `Css`)
- Pagination uses `pageIndex`/`pageSize` static fields on the page class
- `WhereParams` carries filter state, passed through all query layers
- `Dictionaries` class (`Services` project) provides `Dictionary<string, string>` for dropdown population
- `Constantes` class (`Services` project) reads system parameters from DB (e.g., IVA rate, default price list)

## Culture

Application is configured for `es-EC` (Ecuador Spanish). Date formats, decimal separators, and tax logic follow Ecuadorian standards. The SRI module handles electronic invoicing (RIDE, XML generation, authorization).
