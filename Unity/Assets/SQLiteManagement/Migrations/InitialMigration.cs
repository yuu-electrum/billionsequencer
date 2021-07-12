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
    player_name STRING DEFAULT 'sayoko_takayama'
);

CREATE TABLE chart_profiles
(
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    chart_hash STRING,
    title STRING,
    artist STRING,
    lane_count INTEGER NOT NULL,
    level INTEGER NOT NULL,
    min_bpm REAL NOT NULL,
    max_bpm REAL NOT NULL,
    sequence_designer STRING,
    FOREIGN KEY(chart_hash) REFERENCES chart_hashes(chart_hash)
);
";
                }
            }
        }
    }
}