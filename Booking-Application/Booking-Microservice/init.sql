-- SQL Script til at oprette 'bookings' tabellen i PostgreSQL

CREATE TABLE bookings (
    id SERIAL PRIMARY KEY,

    user_id INTEGER NOT NULL,

    court_id VARCHAR(255) NOT NULL,

    total_price NUMERIC(10, 2) NOT NULL,

    start_time TIMESTAMP WITH TIME ZONE NOT NULL,

    end_time TIMESTAMP WITH TIME ZONE NOT NULL,

    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL
);

ALTER TABLE bookings
ADD CONSTRAINT EXCL_CourtTimeOverlap EXCLUDE USING GIST (
    court_id WITH =,                           -- The constraint applies to the same court
    tsrange(start_time, end_time) WITH &&      -- And the time intervals must not overlap
);
