using System;
using System.Xml.Serialization;
using EVEMon.Common.Extensions;

namespace EVEMon.Common.Serialization.Eve
{
    /// <summary>
    /// Represents a serializable version of a characters' skill in training. Used for querying CCP.
    /// </summary>
    public sealed class SerializableAPISkillInTraining : ISynchronizableWithLocalClock
    {
        [XmlElement("currentTQTime")]
        public string CurrentTQTime { get; set; }

        [XmlElement("trainingEndTime")]
        public string TrainingEndTime { get; set; }

        [XmlElement("trainingStartTime")]
        public string TrainingStartTime { get; set; }

        [XmlElement("trainingTypeID")]
        public int TrainingTypeID { get; set; }

        [XmlElement("trainingStartSP")]
        public int TrainingStartSP { get; set; }

        [XmlElement("trainingDestinationSP")]
        public int TrainingDestinationSP { get; set; }

        [XmlElement("trainingToLevel")]
        public byte TrainingToLevel { get; set; }

        [XmlElement("skillInTraining")]
        public byte SkillInTraining { get; set; }

        [XmlIgnore]
        public DateTime CurrentServerTime
        {
            get { return CurrentTQTime.TimeStringToDateTime(); }
            set { CurrentTQTime = value.DateTimeToTimeString(); }
        }

        [XmlIgnore]
        public DateTime StartTime
        {
            get { return TrainingStartTime.TimeStringToDateTime(); }
            set { TrainingStartTime = value.DateTimeToTimeString(); }
        }

        [XmlIgnore]
        public DateTime EndTime
        {
            get { return TrainingEndTime.TimeStringToDateTime(); }
            set { TrainingEndTime = value.DateTimeToTimeString(); }
        }


        #region ISynchronizableWithLocalClock Members

        /// <summary>
        /// Synchronizes the stored times with local clock
        /// </summary>
        /// <param name="drift"></param>
        void ISynchronizableWithLocalClock.SynchronizeWithLocalClock(TimeSpan drift)
        {
            if (!String.IsNullOrEmpty(TrainingStartTime))
                StartTime -= drift;

            if (!String.IsNullOrEmpty(TrainingEndTime))
                EndTime -= drift;
        }

        #endregion
    }
}