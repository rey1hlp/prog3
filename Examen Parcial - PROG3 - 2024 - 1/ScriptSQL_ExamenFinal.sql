DROP TABLE IF EXISTS integrante_grupo_investigacion;
DROP TABLE IF EXISTS profesor;
DROP TABLE IF EXISTS estudiante;
DROP TABLE IF EXISTS miembro_pucp;
DROP TABLE IF EXISTS tipo_miembro_pucp;
DROP TABLE IF EXISTS grupo_investigacion;
DROP TABLE IF EXISTS departamento_academico;
CREATE TABLE departamento_academico(
	id_departamento_academico INT AUTO_INCREMENT,
    nombre VARCHAR(500),
    activo TINYINT,
    PRIMARY KEY(id_departamento_academico)
)ENGINE=InnoDB;
CREATE TABLE grupo_investigacion(
	id_grupo_investigacion INT AUTO_INCREMENT,
    fid_departamento_academico INT,
    nombre VARCHAR(500),
    acronimo VARCHAR(50),
    tipo_investigacion ENUM('BASICA','APLICADA'),
    fecha_fundacion DATE,
    presupuesto_anual_designado DECIMAL(12,2),
    posee_laboratorio TINYINT,
    posee_equipamiento_especializado TINYINT,
    posee_ambiente_trabajo TINYINT,
    descripcion VARCHAR(3000),
    foto LONGBLOB,
    activo TINYINT,
    PRIMARY KEY(id_grupo_investigacion),
    FOREIGN KEY(fid_departamento_academico) REFERENCES departamento_academico(id_departamento_academico)
)ENGINE=InnoDB;
CREATE TABLE tipo_miembro_pucp(
	id_tipo_miembro_pucp CHAR,
    descripcion VARCHAR(100),
    PRIMARY KEY(id_tipo_miembro_pucp)
)ENGINE=InnoDB;
CREATE TABLE miembro_pucp(
	id_miembro_pucp INT AUTO_INCREMENT,
    fid_tipo_miembro_pucp CHAR,
    codigo_pucp VARCHAR(8),
    nombre VARCHAR(200),
    apellido_paterno VARCHAR(200),
    PRIMARY KEY (id_miembro_pucp),
    FOREIGN KEY(fid_tipo_miembro_pucp) REFERENCES tipo_miembro_pucp(id_tipo_miembro_pucp)
)ENGINE=InnoDB;
CREATE TABLE profesor(
	id_profesor INT,
    dedicacion VARCHAR(5),
    activo TINYINT,
    PRIMARY KEY(id_profesor),
    FOREIGN KEY (id_profesor) REFERENCES miembro_pucp(id_miembro_pucp)
)ENGINE=InnoDB;
CREATE TABLE estudiante(
	id_estudiante INT,
    CRAEST DECIMAL(10,2),
    activo TINYINT,
    PRIMARY KEY(id_estudiante),
    FOREIGN KEY (id_estudiante) REFERENCES miembro_pucp(id_miembro_pucp)
)ENGINE=InnoDB;
CREATE TABLE integrante_grupo_investigacion(
	id_integrante_grupo_investigacion INT AUTO_INCREMENT,
    fid_grupo_investigacion INT,
    fid_miembro_pucp INT,
    activo TINYINT,
    PRIMARY KEY(id_integrante_grupo_investigacion),
    FOREIGN KEY(fid_grupo_investigacion) REFERENCES grupo_investigacion(id_grupo_investigacion),
    FOREIGN KEY(fid_miembro_pucp) REFERENCES miembro_pucp(id_miembro_pucp)
)ENGINE=InnoDB;
-- Insertando Tipos de Miembros PUCP
INSERT INTO tipo_miembro_pucp(id_tipo_miembro_pucp, descripcion) VALUES('P','PROFESOR');
INSERT INTO tipo_miembro_pucp(id_tipo_miembro_pucp, descripcion) VALUES('E','ESTUDIANTE');
-- Insertando Profesores
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','02400045','HEIDER','SANCHEZ');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TPA',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','20112728','FREDDY','PAZ');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TC',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','19960275','LUIS','FLORES');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TC',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','00002179','LUIS','RIOS');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TC',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','19931850','MANUEL','TUPIA');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TC',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','00009334','HILMAR','HINOJOSA');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TPA',1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('P','20099362','ARTURO','MOQUILLAZA');
SET @id_profesor = @@last_insert_id;
INSERT INTO profesor(id_profesor,dedicacion,activo) VALUES(@id_profesor,'TPA',1);
-- Insertando Estudiantes
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('E','20150480','GLENN','LOZANO');
SET @id_estudiante = @@last_insert_id;
INSERT INTO estudiante(id_estudiante,CRAEST,activo) VALUES(@id_estudiante,79.00,1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('E','20184255','RONNY','HUERTA');
SET @id_estudiante = @@last_insert_id;
INSERT INTO estudiante(id_estudiante,CRAEST,activo) VALUES(@id_estudiante,88.26,1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('E','20224078','LAURE','SCHLESINGER');
SET @id_estudiante = @@last_insert_id;
INSERT INTO estudiante(id_estudiante,CRAEST,activo) VALUES(@id_estudiante,92.14,1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('E','20245189','SEBASTIAN','MIRANDA');
SET @id_estudiante = @@last_insert_id;
INSERT INTO estudiante(id_estudiante,CRAEST,activo) VALUES(@id_estudiante,89.77,1);
INSERT INTO miembro_pucp(fid_tipo_miembro_pucp,codigo_pucp,nombre,apellido_paterno) VALUES('E','20183872','JHONNY','ESPINOZA');
SET @id_estudiante = @@last_insert_id;
INSERT INTO estudiante(id_estudiante,CRAEST,activo) VALUES(@id_estudiante,91.11,1);
-- Insertando Departamentos Académicos
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE CIENCIAS',1);
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE INGENIERIA',1);
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE PSICOLOGIA',1);
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE EDUCACION',1);
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE DERECHO',1);
INSERT INTO departamento_academico(nombre,activo) VALUES('DEPARTAMENTO DE ECONOMIA',1);
-- Eliminando los Procedimientos Almacenados
DROP PROCEDURE IF EXISTS LISTAR_DEPARTAMENTOS_ACADEMICOS_TODOS;
DROP PROCEDURE IF EXISTS LISTAR_MIEMBROS_PUCP_X_NOMBRE_CODIGOPUCP;
DROP PROCEDURE IF EXISTS INSERTAR_GRUPO_INVESTIGACION;
DROP PROCEDURE IF EXISTS INSERTAR_INTEGRANTE_GRUPO_INVESTIGACION;
DROP PROCEDURE IF EXISTS LISTAR_GRUPOS_INVESTIGACION_X_NOMBRE_ACRONIMO;
DROP PROCEDURE IF EXISTS OBTENER_GRUPO_INVESTIGACION_X_ID;
DROP PROCEDURE IF EXISTS LISTAR_INTEGRANTES_X_ID_GRUPO_INVESTIGACION;
-- Creando los Procedimientos Almacenados
DELIMITER $
CREATE PROCEDURE LISTAR_DEPARTAMENTOS_ACADEMICOS_TODOS()
BEGIN
	SELECT id_departamento_academico, nombre FROM departamento_academico WHERE activo = 1;
END$
CREATE PROCEDURE LISTAR_MIEMBROS_PUCP_X_NOMBRE_CODIGOPUCP(
	IN _nombre_codigoPUCP VARCHAR(400)
)
BEGIN
	SELECT mp.id_miembro_pucp, mp.fid_tipo_miembro_pucp, mp.codigo_pucp, mp.nombre, mp.apellido_paterno, p.dedicacion, e.CRAEST FROM miembro_pucp mp LEFT JOIN profesor p ON mp.id_miembro_pucp = p.id_profesor LEFT JOIN estudiante e ON mp.id_miembro_pucp = e.id_estudiante WHERE (e.activo = 1 OR p.activo = 1) AND ((CONCAT(mp.nombre,' ',mp.apellido_paterno) LIKE CONCAT('%',_nombre_codigoPUCP,'%')) OR (mp.codigo_pucp LIKE CONCAT('%',_nombre_codigoPUCP,'%')));
END$
CREATE PROCEDURE INSERTAR_GRUPO_INVESTIGACION(
	OUT _id_grupo_investigacion INT,
    IN _fid_departamento_academico INT,
    IN _nombre VARCHAR(500),
    IN _acronimo VARCHAR(50),
    IN _tipo_investigacion enum('BASICA','APLICADA'),
    IN _fecha_fundacion DATE,
    IN _presupuesto_anual_designado DECIMAL(12,2),
    IN _posee_laboratorio TINYINT,
    IN _posee_equipamiento_especializado TINYINT,
    IN _posee_ambiente_trabajo TINYINT,
    IN _descripcion VARCHAR(3000),
    IN _foto LONGBLOB
)
BEGIN
	INSERT INTO grupo_investigacion(fid_departamento_academico,nombre,acronimo,tipo_investigacion,fecha_fundacion,presupuesto_anual_designado,posee_laboratorio,posee_equipamiento_especializado,posee_ambiente_trabajo,descripcion,foto,activo) VALUES(_fid_departamento_academico,_nombre,_acronimo,_tipo_investigacion,_fecha_fundacion,_presupuesto_anual_designado,_posee_laboratorio,_posee_equipamiento_especializado,_posee_ambiente_trabajo,_descripcion,_foto,1);
    SET _id_grupo_investigacion = @@last_insert_id;
END$
DELIMITER $
CREATE PROCEDURE INSERTAR_INTEGRANTE_GRUPO_INVESTIGACION(
	OUT _id_integrante_grupo_investigacion INT,
    IN _fid_grupo_investigacion INT,
    IN _fid_miembro_pucp INT
)
BEGIN
	INSERT INTO integrante_grupo_investigacion(fid_grupo_investigacion,fid_miembro_pucp,activo) VALUES(_fid_grupo_investigacion,_fid_miembro_pucp,1);
    SET _id_integrante_grupo_investigacion = @@last_insert_id;
END$
DELIMITER $
CREATE PROCEDURE LISTAR_GRUPOS_INVESTIGACION_X_NOMBRE_ACRONIMO(
	IN _nombre_acronimo VARCHAR(500)
)
BEGIN
	SELECT id_grupo_investigacion, acronimo, nombre FROM grupo_investigacion WHERE activo = 1 AND ((nombre LIKE CONCAT('%',_nombre_acronimo,'%')) OR (acronimo LIKE CONCAT('%',_nombre_acronimo,'%')));
END$
DELIMITER $
CREATE PROCEDURE OBTENER_GRUPO_INVESTIGACION_X_ID(
	IN _id_grupo_investigacion INT
)
BEGIN
	SELECT gi.id_grupo_investigacion, da.id_departamento_academico, da.nombre as nombre_departamento_academico, gi.nombre as nombre_grupo, gi.acronimo, gi.tipo_investigacion, gi.fecha_fundacion, gi.presupuesto_anual_designado, gi.posee_laboratorio, gi.posee_equipamiento_especializado, gi.posee_ambiente_trabajo, gi.descripcion, gi.foto FROM grupo_investigacion gi INNER JOIN departamento_academico da ON gi.fid_departamento_academico = da.id_departamento_academico WHERE gi.activo = 1 and gi.id_grupo_investigacion = _id_grupo_investigacion;
END$
DELIMITER $
CREATE PROCEDURE LISTAR_INTEGRANTES_X_ID_GRUPO_INVESTIGACION(
	IN _id_grupo_investigacion INT
)
BEGIN
	SELECT mp.id_miembro_pucp, mp.fid_tipo_miembro_pucp, mp.codigo_pucp, mp.nombre, mp.apellido_paterno, p.dedicacion, e.CRAEST 
    FROM integrante_grupo_investigacion igi 
    INNER JOIN miembro_pucp mp ON igi.fid_miembro_pucp = mp.id_miembro_pucp 
    LEFT JOIN profesor p ON mp.id_miembro_pucp = p.id_profesor 
    LEFT JOIN estudiante e ON mp.id_miembro_pucp = e.id_estudiante 
    WHERE igi.fid_grupo_investigacion = _id_grupo_investigacion  and igi.activo = 1;
    
END$