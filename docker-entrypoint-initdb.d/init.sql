CREATE DATABASE "avaliacoesDb";
GRANT ALL PRIVILEGES ON DATABASE "avaliacoesDb" TO postgres;

\c avaliacoesDb

-- Only create tables that don't conflict with EF Core migrations
CREATE TABLE IF NOT EXISTS "Events" (
    "Id" serial PRIMARY KEY,
    "Name" text NOT NULL
);

-- Insert static data
INSERT INTO "Events" ("Name") VALUES 
('Evento de Abertura'),
('Workshop de Tecnologia'),
('Feira de Ciências');