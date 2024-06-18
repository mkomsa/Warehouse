CREATE OR REPLACE FUNCTION update_product_quantity() 
RETURNS TRIGGER AS $$
BEGIN
    -- Decrease the available_amount of the product by 1
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
        --on INSERT
        INSERT INTO order_audit (order_id, action, new_status)
        VALUES (NEW.order_id, TG_OP, NEW.status);
    ELSIF TG_OP = 'UPDATE' THEN
        --on status UPDATE
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
																	