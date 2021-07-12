using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using UnityEngine;

namespace ChartManagement
{
    /// <summary>
    /// 譜面フォーマット定義クラス
    /// </summary>
    [DataContract]
    public class Chart
    {
        [DataMember(Name = "global_configurations")]
        public GlobalConfigurations GlobalConfigurations { get; set; }

        [DataMember(Name = "measure_magnifications")]
        public MeasureMagnification[] MeasureMagnifications { get; set; }

        [DataMember(Name = "bpm_transitions")]
        public BpmTransition[] BpmTransitions { get; set; }

        [DataMember(Name = "notes")]
        public Note[] Notes { get; set; }
    }

    [DataContract]
    public class GlobalConfigurations
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        [DataMember(Name = "sequence_designer")]
        public string SequenceDesigner { get; set; }

        [DataMember(Name = "artwork")]
        public string Artwork { get; set; }

        [DataMember(Name = "lane_count")]
        public int LaneCount { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "background_sound_file")]
        public string BackgroundSoundFile { get; set; }

        [DataMember(Name = "gauge_replenishment_base_amount")]
        public int GaugeReplenishmentBaseAmount { get; set; }

        [DataMember(Name = "required_precision_for_judgement")]
        public string RequiredPrecisionForJudgement { get; set; }
    }

    [DataContract]
    public class MeasureMagnification
    {
        [DataMember(Name = "measure")]
        public int Measure { get; set; }

        [DataMember(Name = "magnification")]
        public double Magnification { get; set; }
    }

    [DataContract]
    public class BpmTransition
    {
        [DataMember(Name = "measure")]
        public int Measure { get; set; }

        [DataMember(Name = "bpm")]
        public double Bpm { get; set; }
    }

    [DataContract]
    public class Note
    {
        [DataMember(Name = "measure")]
        public int Measure { get; set; }

        [DataMember(Name = "beat")]
        public int Beat { get; set; }

        [DataMember(Name = "beat_unit")]
        public int BeatUnit { get; set; }

        [DataMember(Name = "lane")]
        public int Lane { get; set; }

        [DataMember(Name = "spawn_lane")]
        public int SpawnLane { get; set; }

        [DataMember(Name = "velocity_multiplier")]
        public double VelocityMultiplier { get; set; }

        [DataMember(Name = "starts_long")]
        public bool StartsLong { get; set; }

        [DataMember(Name = "connects_with")]
        public int ConnectsWith { get; set; }

        [DataMember(Name = "flick")]
        public NotesFlick Flick { get; set; }

        [DataMember(Name = "effect")]
        public NotesEffect Effect { get; set; }
    }

    [DataContract]
    public class NotesEffectParameters
    {
        [DataMember(Name = "concentrates_lanes")]
        public bool ConcentratesLanes { get; set; }
    }

    [DataContract]
    public class NotesEffect
    {
        [DataMember(Name = "drawer")]
        public string Drawer { get; set; }

        [DataMember(Name = "parameters")]
        public NotesEffectParameters Parameters { get; set; }
    }

    [DataContract]
    public class NotesFlick
    {
        [DataMember(Name = "direction")]
        public string Direction { get; set; }
    }
}