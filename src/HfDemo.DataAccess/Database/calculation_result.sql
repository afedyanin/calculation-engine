-- Table: public.calculation_result

-- DROP TABLE IF EXISTS public.calculation_result;

CREATE TABLE IF NOT EXISTS public.calculation_result
(
    id uuid NOT NULL,
    job_id uuid NOT NULL,
    creation_time timestamp with time zone NOT NULL,
    name text COLLATE pg_catalog."default" NOT NULL,
    type text COLLATE pg_catalog."default" NOT NULL,
    metadata_json jsonb,
    result_json jsonb NOT NULL,
    is_completed boolean NOT NULL,
    message text COLLATE pg_catalog."default",
    CONSTRAINT calculation_result_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.calculation_result
    OWNER to postgres;