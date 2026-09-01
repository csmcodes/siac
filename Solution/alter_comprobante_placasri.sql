-- ============================================================
-- ALTER TABLE comprobante - columna para placa SRI (Anexo 25, Resolucion NAC-DGERCGC26-00000024)
-- Generado: 2026-08-27
-- INSTRUCCIONES:
--   - Ejecutar UNA vez por ambiente (dev/prod) y por proveedor de BD activo
--     (ver appSetting "provider" en WebUI/Web.config: SqlServer o PostgreSQL)
--   - No requiere downtime, es columna nullable sin default
-- ============================================================

-- ---- PostgreSQL ----
ALTER TABLE comprobante ADD COLUMN IF NOT EXISTS com_placasri varchar(20);

-- ---- SQL Server ----
-- IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('comprobante') AND name = 'com_placasri')
--     ALTER TABLE comprobante ADD com_placasri varchar(20) NULL;
