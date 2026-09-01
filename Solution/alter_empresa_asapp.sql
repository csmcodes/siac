-- ============================================================
-- ALTER TABLE empresa / comprobante - columnas para integracion Asapp Electronic v2
-- Generado: 2026-07-09
-- INSTRUCCIONES:
--   - Ejecutar UNA vez por ambiente (dev/prod) y por proveedor de BD activo
--     (ver appSetting "provider" en WebUI/Web.config: SqlServer o PostgreSQL)
--   - No requiere downtime, son columnas nullable sin default
-- ============================================================

-- ---- PostgreSQL ----
ALTER TABLE empresa ADD COLUMN IF NOT EXISTS emp_asappapikeyprod varchar(200);
ALTER TABLE empresa ADD COLUMN IF NOT EXISTS emp_asappapikeypruebas varchar(200);

ALTER TABLE comprobante ADD COLUMN IF NOT EXISTS com_provider varchar(20);
ALTER TABLE comprobante ADD COLUMN IF NOT EXISTS com_asappid varchar(50);

-- ---- SQL Server ----
-- IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('empresa') AND name = 'emp_asappapikeyprod')
--     ALTER TABLE empresa ADD emp_asappapikeyprod varchar(200) NULL;
-- IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('empresa') AND name = 'emp_asappapikeypruebas')
--     ALTER TABLE empresa ADD emp_asappapikeypruebas varchar(200) NULL;
-- IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('comprobante') AND name = 'com_provider')
--     ALTER TABLE comprobante ADD com_provider varchar(20) NULL;
-- IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('comprobante') AND name = 'com_asappid')
--     ALTER TABLE comprobante ADD com_asappid varchar(50) NULL;
