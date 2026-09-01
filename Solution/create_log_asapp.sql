-- ============================================================
-- Tabla de log dedicada a la integracion Asapp Electronic v2
-- Generado: 2026-07-10
-- INSTRUCCIONES:
--   - Ejecutar UNA vez por ambiente (dev/prod) y por proveedor de BD activo
--     (ver appSetting "provider" en WebUI/Web.config: SqlServer o PostgreSQL)
--   - Retencion acordada: 12 meses. Purga via WebMethod ws/Metodos.asmx/PurgarLogAsapp
--     (BusinessLogicLayer/LogAsappBLL.cs), pensado para dispararse desde un scheduler
--     externo (Windows Task Scheduler / cron) ya que este proyecto no tiene jobs propios.
-- ============================================================

-- ---- PostgreSQL ----
CREATE TABLE IF NOT EXISTS log_asapp (
    log_codigo BIGSERIAL PRIMARY KEY,
    log_empresa INT NOT NULL,
    log_comprobante BIGINT,
    log_operacion VARCHAR(20),
    log_endpoint VARCHAR(300),
    log_httpstatus INT,
    log_exitoso INT,
    log_request TEXT,
    log_response TEXT,
    log_mensaje VARCHAR(500),
    log_duracionms INT,
    crea_fecha TIMESTAMP DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS idx_log_asapp_comprobante ON log_asapp(log_empresa, log_comprobante);
CREATE INDEX IF NOT EXISTS idx_log_asapp_fecha ON log_asapp(crea_fecha);

-- ---- SQL Server ----
-- IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='log_asapp' AND xtype='U')
-- BEGIN
--     CREATE TABLE log_asapp (
--         log_codigo BIGINT IDENTITY(1,1) PRIMARY KEY,
--         log_empresa INT NOT NULL,
--         log_comprobante BIGINT NULL,
--         log_operacion VARCHAR(20) NULL,
--         log_endpoint VARCHAR(300) NULL,
--         log_httpstatus INT NULL,
--         log_exitoso INT NULL,
--         log_request VARCHAR(MAX) NULL,
--         log_response VARCHAR(MAX) NULL,
--         log_mensaje VARCHAR(500) NULL,
--         log_duracionms INT NULL,
--         crea_fecha DATETIME NULL DEFAULT GETDATE()
--     );
--     CREATE INDEX idx_log_asapp_comprobante ON log_asapp(log_empresa, log_comprobante);
--     CREATE INDEX idx_log_asapp_fecha ON log_asapp(crea_fecha);
-- END
