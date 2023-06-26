export interface SqliteMigration {
  id: number;
  queries: Array<string>;
}

export class SqliteMigrations {
  private static readonly migrations: Array<SqliteMigration> = [
    {
      id: 1,
      queries: [`CREATE TABLE Tracking_copy(
        id TEXT,
        timestamp NUMERIC,
        longitude DOUBLE,
        latitude DOUBLE,
        trappingTypeId TEXT,
        sessionId TEXT,
        isTimewriting NUMERIC,
        isTrackingMap NUMERIC,
        isTrackingPrivate NUMERIC,
      PRIMARY KEY (id)
      );
      `,
        `
      INSERT INTO Tracking_copy (id,timestamp,longitude,latitude,trappingTypeId,sessionId,isTimewriting,isTrackingMap,isTrackingPrivate)
        SELECT id, timestamp, longitude, latitude, trappingTypeId, sessionId, isTimewriting, isTrackingMap, isTrackingPrivate FROM Tracking;
      `,
        `
      DROP TABLE Tracking;
      `,
        `
      ALTER TABLE Tracking_copy RENAME TO Tracking;
      `
      ]
    },
    {
      id: 2,
      queries: [`
        DELETE FROM Tracking WHERE trappingTypeId IS NULL OR sessionId IS NULL;
      `]
    }
  ];

  public static getNewMigrations(lastMigrationId: number): Array<SqliteMigration> {
    return this.migrations.filter(m => m.id > lastMigrationId);
  }

  public static get lastMigrationId(): number {
    return SqliteMigrations.migrations.length > 0 ? SqliteMigrations.migrations[SqliteMigrations.migrations.length - 1].id : 0;
  }
}
