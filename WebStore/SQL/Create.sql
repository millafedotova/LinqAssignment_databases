-- 1. customers
CREATE TABLE customers (
    customer_id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    created_at TIMESTAMP WITHOUT TIME ZONE,
    updated_at TIMESTAMP WITHOUT TIME ZONE
);

-- 11. carriers
CREATE TABLE carriers (
    carrier_id SERIAL PRIMARY KEY,
    carrier_name VARCHAR(50) NOT NULL,
    contact_url VARCHAR(50),
    contact_phone VARCHAR(50)
);

-- 12. Добавляем новые поля в таблицу orders
ALTER TABLE orders
ADD COLUMN carrier_id INTEGER,
ADD COLUMN tracking_number VARCHAR(50),
ADD COLUMN shipped_date TIMESTAMP WITHOUT TIME ZONE,
ADD COLUMN delivered_date TIMESTAMP WITHOUT TIME ZONE,
ADD CONSTRAINT fk_orders_carrier FOREIGN KEY (carrier_id) REFERENCES carriers (carrier_id) ON UPDATE CASCADE ON DELETE SET NULL; 