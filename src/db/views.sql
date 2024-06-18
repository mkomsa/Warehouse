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

   
CREATE VIEW public."OrderDetailView" AS
SELECT
    o.order_id AS "order_id",
    o.customer_id AS "customer_id",
    o.address_id AS "order_address_id",
    o.invoice_id AS "invoice_id",
    ce.address_id AS "customer_address_id",
    ce.name AS "customer_name",
    ce.full_name AS "customer_full_name",
    ce.email AS "customer_email",
    ce.phone_number AS "customer_phone_number",
    ca.postal_code AS "customer_address_postal_code",
    ca.street AS "customer_address_street",
    ca.apartment AS "customer_address_apartment",
    ia.postal_code AS "order_address_postal_code",
    ia.street AS "order_address_street",
    ia.apartment AS "order_address_apartment",
    ie.transaction_date AS "invoice_transaction_date",
    ie.net_value AS "invoice_net_value",
    ie.gross_value AS "invoice_gross_value",
    ie.status AS "invoice_status",
    ie.vat_rate AS "invoice_vat_rate",
    op.order_product_id AS "order_product_id",
    op.order_id AS "order_product_order_id",
    op.product_id AS "order_product_product_id",
    p.product_id AS "product_id",
    p."name"  AS "product_name",
    p.manufacturer_id AS "product_manufacturer_id",
    p.parcel_info_id AS "product_parcel_info_id",
    p.available_amount AS "product_available_amount",
    p.price AS "product_price",
    ma.manufacturer_id AS "manufacturer_id",
    ma.address_id AS "manufacturer_address_id",
    ma.name AS "manufacturer_name",
    ma.email AS "manufacturer_email",
    ma.phone_number AS "manufacturer_phone_number",
    ma_address.postal_code AS "manufacturer_address_postal_code",
    ma_address.street AS "manufacturer_address_street",
    ma_address.apartment AS "manufacturer_address_apartment",
    pa.parcel_info_id AS "parcel_info_id",
    pa.weight AS "parcel_info_weight",
    pa.height AS "parcel_info_height",
    pa.length AS "parcel_info_length",
    pa.width AS "parcel_info_width"
FROM
    "public"."order" AS o
LEFT JOIN
    "public"."customer" AS ce ON o.customer_id = ce.customer_id
LEFT JOIN
    "public"."address" AS ca ON ce.address_id = ca.address_id
LEFT JOIN
    "public"."address" AS ia ON o.address_id = ia.address_id
LEFT JOIN
    "public"."invoice" AS ie ON o.invoice_id = ie.invoice_id
LEFT JOIN
    "public"."order_product" AS op ON o.order_id = op.order_id
LEFT JOIN
    "public"."product" AS p ON op.product_id = p.product_id
LEFT JOIN
    "public"."manufacturer" AS ma ON p.manufacturer_id = ma.manufacturer_id
LEFT JOIN
    "public"."address" AS ma_address ON ma.address_id = ma_address.address_id
LEFT JOIN
    "public"."parcel_info" AS pa ON p.parcel_info_id = pa.parcel_info_id;

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