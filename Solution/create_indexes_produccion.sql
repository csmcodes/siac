-- ============================================================
-- SCRIPT DE INDICES EN PRODUCCION - siacCOMY (o BD prod)
-- Generado: 2026-07-01
-- INSTRUCCIONES:
--   - Ejecutar conectado directamente a la BD de produccion
--   - NO envolver en BEGIN/COMMIT — CONCURRENTLY no funciona
--     dentro de transacciones
--   - Los usuarios pueden seguir trabajando durante la ejecucion
--   - Puede tardar varios minutos por tabla grande (normal)
--   - IF NOT EXISTS evita error si algun indice ya existe
-- ============================================================

-- ============================================================
-- PRIORIDAD 1 (CRITICA): comprobantehistorial
-- 4.2 GB / ~1.8M filas
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comphist_codigo
    ON comprobantehistorial(coh_empresa, coh_codigo);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comphist_fecha
    ON comprobantehistorial(coh_empresa, coh_fecha DESC);

-- ============================================================
-- PRIORIDAD 1 (CRITICA): comprobante — filtros del listado
-- ~390 MB / ~1.2M filas
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_estado_fecha
    ON comprobante(com_empresa, com_estado, com_fecha DESC);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_tipodoc_fecha
    ON comprobante(com_empresa, com_tipodoc, com_fecha DESC);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_tipodoc
    ON comprobante(com_empresa, com_tipodoc);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_periodo
    ON comprobante(com_empresa, com_periodo, com_mes);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_codclipro
    ON comprobante(com_empresa, com_codclipro);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_ctipocom
    ON comprobante(com_empresa, com_ctipocom);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_almacen
    ON comprobante(com_empresa, com_almacen);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_comprobante_ref
    ON comprobante(com_empresa, com_ref_comprobante);

-- ============================================================
-- PRIORIDAD 1 (CRITICA): indices para los JOIN del listado
-- Eliminan seq scans de 630K-1.17M filas en cada consulta
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomenv_comprobante
    ON ccomenv(cenv_comprobante);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_total_comprobante
    ON total(tot_comprobante);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ddocumento_comprob
    ON ddocumento(ddo_comprobante);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomdoc_comprobante
    ON ccomdoc(cdoc_comprobante);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dbancario_comprob
    ON dbancario(dban_cco_comproba);

-- ============================================================
-- PRIORIDAD 2 (ALTA): dcontable — reportes contables
-- ~507 MB / ~2.6M filas
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dcontable_cuenta
    ON dcontable(dco_empresa, dco_cuenta);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dcontable_cliente
    ON dcontable(dco_empresa, dco_cliente);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dcontable_fecha_vence
    ON dcontable(dco_empresa, dco_fecha_vence);

-- ============================================================
-- PRIORIDAD 2 (ALTA): ccomenv — hojas de ruta / envios
-- ~264 MB / ~630K filas
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomenv_socio
    ON ccomenv(cenv_empresa, cenv_socio);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomenv_ruta
    ON ccomenv(cenv_empresa, cenv_ruta);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomenv_vehiculo
    ON ccomenv(cenv_empresa, cenv_vehiculo);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ccomenv_chofer
    ON ccomenv(cenv_empresa, cenv_chofer);

-- ============================================================
-- PRIORIDAD 3 (MEDIA): tablas de detalle y CxC/CxP
-- ============================================================

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dcomdoc_producto
    ON dcomdoc(ddoc_empresa, ddoc_producto);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_rutaxfactura_fac
    ON rutaxfactura(rfac_empresa, rfac_comprobantefac);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_dcancelacion_can
    ON dcancelacion(dca_empresa, dca_comprobante_can);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ddocumento_codclipro
    ON ddocumento(ddo_empresa, ddo_codclipro);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ddocumento_guia
    ON ddocumento(ddo_empresa, ddo_comprobante_guia);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_ddocumento_fecha_ven
    ON ddocumento(ddo_empresa, ddo_fecha_ven);

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_drecibo_ref
    ON drecibo(dfp_empresa, dfp_ref_comprobante);

-- ============================================================
-- ACTUALIZACION DE ESTADISTICAS
-- Ejecutar al finalizar todos los indices
-- ============================================================

ANALYZE comprobante;
ANALYZE comprobantehistorial;
ANALYZE dcontable;
ANALYZE ccomenv;
ANALYZE total;
ANALYZE ddocumento;
ANALYZE ccomdoc;
ANALYZE dcancelacion;
ANALYZE drecibo;
ANALYZE dcomdoc;
ANALYZE rutaxfactura;
ANALYZE dbancario;

-- ============================================================
-- VERIFICACION FINAL — muestra todos los indices creados
-- ============================================================

SELECT
    tablename,
    indexname,
    pg_size_pretty(pg_relation_size(schemaname||'.'||indexname)) AS index_size
FROM pg_indexes
WHERE schemaname = 'public'
  AND indexname LIKE 'idx_%'
ORDER BY pg_relation_size(schemaname||'.'||indexname) DESC;
