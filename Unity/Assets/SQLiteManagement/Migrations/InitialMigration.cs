using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SQLiteManagement
{
    namespace Migrations
    {
        /// <summary>
        /// マイグレーションのインターフェース
        /// </summary>
        public class InitialMigration: IMigration
        {
            public int Id
            {
                get
                {
                    return 0;
                }
            }

            public string ExecutionQuery
            {
                get
                {
                    return $@"
CREATE TABLE chart_hashes
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    chart_hash STRING UNIQUE NOT NULL
);

CREATE TABLE players
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    guid STRING NOT NULL,
    player_name STRING DEFAULT '{Constant.SQLite.DefaultPlayerName}'
);

CREATE TABLE chart_profiles
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    chart_hash STRING,
    file_path STRING,
    title STRING,
    artist STRING,
    note_count INTEGER NOT NULL,
    lane_count INTEGER NOT NULL,
    level INTEGER NOT NULL,
    min_bpm REAL NOT NULL,
    max_bpm REAL NOT NULL,
    sequence_designer STRING,
    FOREIGN KEY(chart_hash) REFERENCES chart_hashes(chart_hash)
);

CREATE TABLE score_profiles
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    guid STRING NOT NULL,
    chart_hash STRING NOT NULL,
    score INTEGER NOT NULL,
    FOREIGN KEY (guid) REFERENCES players(guid),
    FOREIGN KEY (chart_hash) REFERENCES chart_profiles(chart_hash)
)
";
                }
            }
        }
    }
}