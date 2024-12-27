-- Table: public.calculation_progress

-- DROP TABLE IF EXISTS public.calculation_progress;

CREATE TABLE IF NOT EXISTS public.calculation_progress
(
    id uuid NOT NULL,
    job_id uuid NOT NULL,
    creation_time timestamp with time zone NOT NULL,
    percent integer NOT NULL,
    message text COLLATE pg_catalog."default",
    CONSTRAINT calculation_progress_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.calculation_progress
    OWNER to postgres;

