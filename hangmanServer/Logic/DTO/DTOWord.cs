using System.Runtime.Serialization;

namespace Logic.DTO
{
    public class DTOWord
    {
        [DataMember]
        public int IdWord { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NameEn { get; set; }

        [DataMember]
        public string Hint { get; set; }

        [DataMember]
        public string HintEn { get; set; }
    }
}
