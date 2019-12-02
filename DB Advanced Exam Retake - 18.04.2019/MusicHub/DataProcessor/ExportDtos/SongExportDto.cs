namespace MusicHub.DataProcessor.ExportDtos
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Song")]
    public class SongExportDto
    {
         public string SongName { get; set; } 

        public string Writer { get; set; }

        public string Performer { get; set; }

        public string AlbumProducer { get; set; }

        public string Duration { get; set; }

    }
}
