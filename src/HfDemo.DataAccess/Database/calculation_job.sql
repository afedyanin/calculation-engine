-- Table: public.calculation_job

-- DROP TABLE IF EXISTS public.calculation_job;

CREATE TABLE IF NOT EXISTS public.calculation_job
(
    id uuid NOT NULL,
    parent_id uuid,
    name text COLLATE pg_catalog."default" NOT NULL,
    type text COLLATE pg_catalog."default" NOT NULL,
    start_time timestamp with time zone NOT NULL,
    end_time timestamp with time zone,
    status integer NOT NULL,
    parameters_json jsonb,
    CONSTRAINT calculation_job_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.calculation_job
    OWNER to postgres;

