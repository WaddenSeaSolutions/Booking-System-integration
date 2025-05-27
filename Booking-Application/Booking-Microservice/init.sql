-- Creating the bookings table with an exclusion constraint to prevent overlapping bookings
CREATE TABLE bookings (
    id SERIAL PRIMARY KEY,

    user_id INTEGER NOT NULL,

    court_id VARCHAR(255) NOT NULL,

    total_price NUMERIC(10, 2) NOT NULL,

    start_time TIMESTAMP WITH TIME ZONE NOT NULL,

    end_time TIMESTAMP WITH TIME ZONE NOT NULL,

    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL
);



--Solution before there was added a transaction which checked if there already was a booking within the timeframe
-- Adding the extension btree_gist for the exclusion constraint
CREATE EXTENSION btree_gist;
-- Adding an exclusion constraint to prevent overlapping bookings for the same court
ALTER TABLE bookings
ADD CONSTRAINT EXCL_CourtTimeOverlap EXCLUDE USING GIST (
    court_id WITH =,
    booking_date WITH =, -- The constraint also applies to the same date
    tstzrange(start_time, end_time) WITH &&
);