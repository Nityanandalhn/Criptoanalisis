--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.2

-- Started on 2022-06-13 21:49:19

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 16432)
-- Name: endpoint; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.endpoint (
    id integer NOT NULL,
    url character varying NOT NULL,
    tipo character varying NOT NULL
);


ALTER TABLE public.endpoint OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16431)
-- Name: endpoint_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.endpoint_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.endpoint_id_seq OWNER TO postgres;

--
-- TOC entry 3376 (class 0 OID 0)
-- Dependencies: 209
-- Name: endpoint_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.endpoint_id_seq OWNED BY public.endpoint.id;


--
-- TOC entry 217 (class 1259 OID 16474)
-- Name: endpoint_parametros; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.endpoint_parametros (
    cod_edp integer NOT NULL,
    cod_prm integer NOT NULL
);


ALTER TABLE public.endpoint_parametros OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16450)
-- Name: intercambio; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.intercambio (
    id integer NOT NULL,
    nombre character varying NOT NULL,
    fecha timestamp with time zone,
    intercambiado character varying NOT NULL,
    volumen numeric,
    abierto numeric,
    alto numeric,
    bajo numeric,
    reciente numeric,
    cod_edp integer
);


ALTER TABLE public.intercambio OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 24667)
-- Name: intercambio_usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.intercambio_usuario (
    cod_usr integer NOT NULL,
    cod_itc integer NOT NULL
);


ALTER TABLE public.intercambio_usuario OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 24702)
-- Name: moneda; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.moneda (
    nombre character varying NOT NULL
);


ALTER TABLE public.moneda OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16449)
-- Name: moneda_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.moneda_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.moneda_id_seq OWNER TO postgres;

--
-- TOC entry 3377 (class 0 OID 0)
-- Dependencies: 213
-- Name: moneda_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.moneda_id_seq OWNED BY public.intercambio.id;


--
-- TOC entry 212 (class 1259 OID 16441)
-- Name: parametros; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.parametros (
    id integer NOT NULL,
    valor character varying NOT NULL,
    mapping_modelo character varying NOT NULL,
    tipo character varying NOT NULL
);


ALTER TABLE public.parametros OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16440)
-- Name: parametros_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.parametros_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.parametros_id_seq OWNER TO postgres;

--
-- TOC entry 3378 (class 0 OID 0)
-- Dependencies: 211
-- Name: parametros_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.parametros_id_seq OWNED BY public.parametros.id;


--
-- TOC entry 216 (class 1259 OID 16459)
-- Name: usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario (
    id integer NOT NULL,
    login character varying,
    pwd character varying,
    mail character varying NOT NULL
);


ALTER TABLE public.usuario OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24682)
-- Name: usuario_endpoint; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario_endpoint (
    cod_usr integer NOT NULL,
    cod_edp integer NOT NULL
);


ALTER TABLE public.usuario_endpoint OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16458)
-- Name: usuario_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.usuario_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.usuario_id_seq OWNER TO postgres;

--
-- TOC entry 3379 (class 0 OID 0)
-- Dependencies: 215
-- Name: usuario_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.usuario_id_seq OWNED BY public.usuario.id;


--
-- TOC entry 221 (class 1259 OID 24736)
-- Name: usuario_moneda; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario_moneda (
    cod_usr integer NOT NULL,
    nom_mnd character varying NOT NULL
);


ALTER TABLE public.usuario_moneda OWNER TO postgres;

--
-- TOC entry 3199 (class 2604 OID 16435)
-- Name: endpoint id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint ALTER COLUMN id SET DEFAULT nextval('public.endpoint_id_seq'::regclass);


--
-- TOC entry 3201 (class 2604 OID 16453)
-- Name: intercambio id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio ALTER COLUMN id SET DEFAULT nextval('public.moneda_id_seq'::regclass);


--
-- TOC entry 3200 (class 2604 OID 16444)
-- Name: parametros id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.parametros ALTER COLUMN id SET DEFAULT nextval('public.parametros_id_seq'::regclass);


--
-- TOC entry 3202 (class 2604 OID 16462)
-- Name: usuario id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario ALTER COLUMN id SET DEFAULT nextval('public.usuario_id_seq'::regclass);


--
-- TOC entry 3204 (class 2606 OID 16439)
-- Name: endpoint endpoint_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint
    ADD CONSTRAINT endpoint_pkey PRIMARY KEY (id);


--
-- TOC entry 3210 (class 2606 OID 16457)
-- Name: intercambio moneda_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio
    ADD CONSTRAINT moneda_pkey PRIMARY KEY (id);


--
-- TOC entry 3208 (class 2606 OID 16448)
-- Name: parametros parametros_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.parametros
    ADD CONSTRAINT parametros_pkey PRIMARY KEY (id);


--
-- TOC entry 3214 (class 2606 OID 16478)
-- Name: endpoint_parametros pk_endpoint_parametros; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint_parametros
    ADD CONSTRAINT pk_endpoint_parametros PRIMARY KEY (cod_edp, cod_prm);


--
-- TOC entry 3216 (class 2606 OID 24681)
-- Name: intercambio_usuario pk_intercambio_usuario; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio_usuario
    ADD CONSTRAINT pk_intercambio_usuario PRIMARY KEY (cod_itc, cod_usr);


--
-- TOC entry 3220 (class 2606 OID 24708)
-- Name: moneda pk_moneda; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.moneda
    ADD CONSTRAINT pk_moneda PRIMARY KEY (nombre);


--
-- TOC entry 3218 (class 2606 OID 24686)
-- Name: usuario_endpoint pk_usuario_endpoint; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_endpoint
    ADD CONSTRAINT pk_usuario_endpoint PRIMARY KEY (cod_edp, cod_usr);


--
-- TOC entry 3222 (class 2606 OID 24742)
-- Name: usuario_moneda pk_usuario_moneda; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_moneda
    ADD CONSTRAINT pk_usuario_moneda PRIMARY KEY (cod_usr, nom_mnd);


--
-- TOC entry 3206 (class 2606 OID 24754)
-- Name: endpoint uk_endpoint; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint
    ADD CONSTRAINT uk_endpoint UNIQUE (url);


--
-- TOC entry 3212 (class 2606 OID 16466)
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);


--
-- TOC entry 3224 (class 2606 OID 16479)
-- Name: endpoint_parametros fk_endpoint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint_parametros
    ADD CONSTRAINT fk_endpoint FOREIGN KEY (cod_edp) REFERENCES public.endpoint(id);


--
-- TOC entry 3223 (class 2606 OID 24662)
-- Name: intercambio fk_endpoint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio
    ADD CONSTRAINT fk_endpoint FOREIGN KEY (cod_edp) REFERENCES public.endpoint(id);


--
-- TOC entry 3228 (class 2606 OID 24692)
-- Name: usuario_endpoint fk_endpoint; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_endpoint
    ADD CONSTRAINT fk_endpoint FOREIGN KEY (cod_edp) REFERENCES public.endpoint(id);


--
-- TOC entry 3227 (class 2606 OID 24675)
-- Name: intercambio_usuario fk_intercambio; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio_usuario
    ADD CONSTRAINT fk_intercambio FOREIGN KEY (cod_itc) REFERENCES public.intercambio(id);


--
-- TOC entry 3230 (class 2606 OID 24743)
-- Name: usuario_moneda fk_moneda; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_moneda
    ADD CONSTRAINT fk_moneda FOREIGN KEY (nom_mnd) REFERENCES public.moneda(nombre);


--
-- TOC entry 3225 (class 2606 OID 16484)
-- Name: endpoint_parametros fk_parametros; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.endpoint_parametros
    ADD CONSTRAINT fk_parametros FOREIGN KEY (cod_prm) REFERENCES public.parametros(id);


--
-- TOC entry 3226 (class 2606 OID 24670)
-- Name: intercambio_usuario fk_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.intercambio_usuario
    ADD CONSTRAINT fk_usuario FOREIGN KEY (cod_usr) REFERENCES public.usuario(id);


--
-- TOC entry 3229 (class 2606 OID 24697)
-- Name: usuario_endpoint fk_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_endpoint
    ADD CONSTRAINT fk_usuario FOREIGN KEY (cod_usr) REFERENCES public.usuario(id);


--
-- TOC entry 3231 (class 2606 OID 24748)
-- Name: usuario_moneda fk_usuario; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario_moneda
    ADD CONSTRAINT fk_usuario FOREIGN KEY (cod_usr) REFERENCES public.usuario(id);


-- Completed on 2022-06-13 21:49:19

--
-- PostgreSQL database dump complete
--

