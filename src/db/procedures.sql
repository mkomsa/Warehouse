CREATE OR REPLACE PROCEDURE add_product(
    IN p_product_id uuid,
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
    --transaction
    BEGIN
        -- random available_amount if not provided
        IF p_available_amount IS NULL THEN
            random_quantity := floor(random() * (15 - 5 + 1) + 5);
        ELSE
            random_quantity := p_available_amount;
        END IF;
        
        -- Insert into product
        INSERT INTO product (product_id, parcel_info_id, manufacturer_id, price, available_amount)
        VALUES (p_product_id, p_parcel_info_id, p_manufacturer_id, p_price, random_quantity);
        
        --Commit
        COMMIT;
    EXCEPTION
        -- Rollback
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END;
END;
$$;
