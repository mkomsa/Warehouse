CREATE VIEW public."order_view" AS
SELECT
    o.order_id AS "order_id",
    o.customer_id AS "customer_id",
    o.address_id AS "address_id",
    o.invoice_id AS "invoice_id",
    o.status as "status",
    ce.address_id AS "customer_address_id",
    ce.name AS "customer_name",
    ce.full_name AS "customer_full_name",
    ce.email AS "customer_email",
    ce.phone_number AS "customer_phone_number",
    ae.postal_code AS "address_postal_code",
    ae.street AS "address_street",
    ae.apartment AS "address_apartment",
    ie.transaction_date AS "invoice_transaction_date",
    ie.net_value AS "invoice_net_value",
    ie.gross_value AS "invoice_gross_value",
    ie.status AS "invoice_status",
    ie.vat_rate AS "invoice_vat_rate",
    op.order_product_id AS "order_product_id",
    op.order_id AS "order_product_order_id",
    op.product_id AS "order_product_product_id",
    p.product_id AS "product_id",
    p.name  AS "product_name",
    p.manufacturer_id AS "product_manufacturer_id",
    p.parcel_info_id AS "product_parcel_info_id",
    p.available_amount AS "product_available_amount",
    p.price AS "product_price",
    ma.manufacturer_id AS "manufacturer_id",
    ma.address_id AS "manufacturer_address_id",
    pea.parcel_info_id AS "parcel_info_id",
    pea.weight AS "parcel_info_weight",
    pea.height AS "parcel_info_height",
    pea.length AS "parcel_info_length"
FROM
    "public"."order" AS o
LEFT JOIN
    "public"."customer" AS ce ON o.customer_id = ce.customer_id
LEFT JOIN
    "public"."address" AS ae ON ce.address_id = ae.address_id
LEFT JOIN
    "public"."invoice" AS ie ON o.invoice_id = ie.invoice_id
LEFT JOIN
    "public"."order_product" AS op ON o.order_id = op.order_id
LEFT JOIN
    "public"."product" AS p ON op.product_id = p.product_id
LEFT JOIN
    "public"."manufacturer" AS ma ON p.manufacturer_id = ma.manufacturer_id
LEFT JOIN
    "public"."parcel_info" AS pea ON p.parcel_info_id = pea.parcel_info_id;

   

																					product:::

CREATE VIEW product_view AS
SELECT 
    p.product_id,
    p.name,
    p.price,
    p.available_amount,
    pi.parcel_info_id,
    pi.length,
    pi.width,
    pi.height,
    pi.weight,
    m.manufacturer_id,
    m.name AS manufacturer_name,
    m.email AS manufacturer_email,
    m.phone_number AS manufacturer_phone,
    a.address_id AS manufacturer_address_id,
    a.postal_code AS manufacturer_postal_code,
    a.street AS manufacturer_street,
    a.apartment AS manufacturer_apartment
FROM product p
JOIN parcel_info pi ON p.parcel_info_id = pi.parcel_info_id
JOIN manufacturer m ON p.manufacturer_id = m.manufacturer_id
JOIN address a ON m.address_id = a.address_id;


																					parcelInfo:::
CREATE VIEW parcel_info_view AS
SELECT 
    parcel_info_id,
    length,
    width,
    height,
    weight
FROM 
    parcel_info;
   
   
																					manufacturer:::
CREATE VIEW manufacturer_view AS
SELECT 
    m.manufacturer_id,
    m.name,
    m.email,
    m.phone_number,
    a.address_id,
    a.postal_code,
    a.street,
    a.apartment
FROM 
    manufacturer m
JOIN 
    address a ON m.address_id = a.address_id;

   
   

																					aggregated customer_orders:::
CREATE VIEW customer_orders_view AS
SELECT 
    c.customer_id,
    c.name,
    SUM(i.gross_value) AS total_spent
FROM 
    customer c
JOIN 
    "order" o ON c.customer_id = o.customer_id
JOIN 
    invoice i ON o.invoice_id = i.invoice_id
GROUP BY 
    c.customer_id, c.name;
   
SELECT * FROM public.customer_orders_view

SELECT order_id, customer_id, address_id, invoice_id, status
FROM public."order";

SELECT product_id, parcel_info_id, manufacturer_id, price, available_amount
FROM public.product;



																product_available_amount:::
CREATE OR REPLACE FUNCTION update_product_quantity() 
RETURNS TRIGGER AS $$
BEGIN
    -- decrease available_amount of product
    UPDATE product 
    SET available_amount = CASE 
                               WHEN available_amount - 1 = 0 THEN 5 
                               ELSE available_amount - 1 
                           END
    WHERE product_id = NEW.product_id;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Create the trigger
CREATE TRIGGER after_order_product_insert
AFTER INSERT ON order_product
FOR EACH ROW
EXECUTE FUNCTION update_product_quantity();



																	order_audit:::
																	
CREATE TABLE order_audit (
    audit_id uuid DEFAULT gen_random_uuid() PRIMARY KEY,
    order_id uuid NOT NULL,
    action character varying(10) NOT NULL,
    changed_at timestamptz NOT NULL DEFAULT now(),
    old_status character varying(16),
    new_status character varying(16)
);

CREATE OR REPLACE FUNCTION audit_order_status()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        -- audit record for INSERTs
        INSERT INTO order_audit (order_id, action, new_status)
        VALUES (NEW.order_id, TG_OP, NEW.status);
    ELSIF TG_OP = 'UPDATE' THEN
        --audit record for UPDATEs
        IF NEW.status IS DISTINCT FROM OLD.status THEN
            INSERT INTO order_audit (order_id, action, old_status, new_status)
            VALUES (NEW.order_id, TG_OP, OLD.status, NEW.status);
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER trg_order_audit_insert
AFTER INSERT ON "order"
FOR EACH ROW
EXECUTE FUNCTION audit_order_status();

CREATE TRIGGER trg_order_audit_update
AFTER UPDATE ON "order"
FOR EACH ROW
EXECUTE FUNCTION audit_order_status();
																	



                                                                    
																		-- CREATE PRODUCT
CREATE OR REPLACE PROCEDURE add_product(
    IN p_product_id uuid,
    IN p_name varchar,
    IN p_parcel_info_id uuid,
    IN p_manufacturer_id uuid,
    IN p_price double precision,
    IN p_available_amount integer DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    random_quantity int;
BEGIN
    -- random available_amount if not provided
    IF p_available_amount IS NULL THEN
        random_quantity := floor(random() * (15 - 5 + 1) + 5);
    ELSE
        random_quantity := p_available_amount;
    END IF;
    
    -- Insert into product
    INSERT INTO product (product_id, name, parcel_info_id, manufacturer_id, price, available_amount)
    VALUES (p_product_id, p_name, p_parcel_info_id, p_manufacturer_id, p_price, random_quantity);
END;
$$;

																		-- UPDATE PRODUCT
CREATE OR REPLACE PROCEDURE update_product(
    IN p_product_id uuid,
    IN p_name varchar,
    IN p_parcel_info_id uuid,
    IN p_manufacturer_id uuid,
    IN p_price double precision,
    IN p_available_amount integer
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Update the product
    UPDATE product
    SET
        name = p_name,
        parcel_info_id = p_parcel_info_id,
        manufacturer_id = p_manufacturer_id,
        price = p_price,
        available_amount = p_available_amount
    WHERE product_id = p_product_id;
END;
$$;


CALL update_product(
    '40424c17-d191-49a7-b4de-d9dfd1b77656',
    'this is updated',
    '412b4fdc-23c7-4406-82aa-b6fe965d434e',
    'ebb3f249-9749-4e54-8a85-aaa421e317a7',
    32,
    12321
);