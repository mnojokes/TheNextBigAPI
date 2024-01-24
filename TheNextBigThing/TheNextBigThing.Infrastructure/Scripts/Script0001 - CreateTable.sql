CREATE TABLE exchange_rates
(
    id serial NOT NULL,
    date date NOT NULL,
    rates character varying NOT NULL,
    PRIMARY KEY (id)
);