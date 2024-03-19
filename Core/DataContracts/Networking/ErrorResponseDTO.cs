using Core.DataContracts.Enums;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Core.DataContracts.Networking
{
    [DataContract]
    public class ErrorResponseDTO(LanHQSystems source, bool isSilent, string description, int? code, string? codeName, StackTrace? stackTrace)
    {
        /// <summary>
        /// The system that generated the error.
        /// </summary>
        [DataMember]
        public LanHQSystems Source { get; } = source;

        /// <summary>
        /// Defines the specific code describing the response. Forms a unique key when paired with the <see cref="Source"/>
        /// </summary>
        [DataMember]
        public int? Code { get; } = code;


        /// <summary>
        /// Represents the error code in a human readable format.
        /// </summary>
        [DataMember]
        public string? CodeName { get; } = codeName;

        /// <summary>
        /// Weather or not the error should be displayed to the users.
        /// </summary>
        [DataMember]
        public bool IsSilent { get; } = isSilent;

        /// <summary>
        /// Describes the error in a human readable format.
        /// </summary>
        [DataMember]
        public string Description { get; } = description;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public StackTrace? StackTrace { get; } = stackTrace;
    }
}
