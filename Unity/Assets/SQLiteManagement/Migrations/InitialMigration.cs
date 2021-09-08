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
    chart_hash STRING,
    PRIMARY KEY (chart_hash),
    UNIQUE (chart_hash)
);

CREATE TABLE players
(
    guid STRING,
    player_name STRING DEFAULT '{Constant.SQLite.DefaultPlayerName}',
    PRIMARY KEY (guid)
);

CREATE TABLE chart_profiles
(
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
    guid STRING NOT NULL,
    chart_hash STRING NOT NULL,
    play_result TEXT COLLATE BINARY NOT NULL DEFAULT 'never_played',
    score INTEGER NOT NULL,
    FOREIGN KEY (guid) REFERENCES players(guid),
    FOREIGN KEY (chart_hash) REFERENCES chart_profiles(chart_hash),
    CHECK(play_result = 'never_played' OR play_result = 'failed' OR play_result = 'succeeded_over_reference' OR play_result = 'succeeded_life_retaining')
)
";
                }
            }
        }
    }
}