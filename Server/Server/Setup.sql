CREATE TABLE IF NOT EXISTS account (
	id UUID NOT NULL PRIMARY KEY,
	displayName TEXT NOT NULL,
	email TEXT NOT NULL UNIQUE,
	password_digest BYTEA NOT NULL
);
CREATE TABLE IF NOT EXISTS document (
    id UUID NOT NULL PRIMARY KEY,
    title TEXT NOT NULL,
    authorId UUID NOT NULL REFERENCES account (id) ON DELETE RESTRICT,
    timestamp TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (current_timestamp AT TIME ZONE 'UTC')
);
CREATE TABLE IF NOT EXISTS documentBody (
    id UUID NOT NULL PRIMARY KEY REFERENCES document (id) ON DELETE RESTRICT,
    body TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS relation (
    antecedentId UUID NOT NULL REFERENCES document (id) ON DELETE RESTRICT,
    descendantId UUID NOT NULL REFERENCES document (id) ON DELETE RESTRICT,
    PRIMARY KEY (antecedentId, descendantId),
    CHECK (antecedentId != descendantId)
);