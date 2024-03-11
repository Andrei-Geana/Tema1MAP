﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Model
{
    internal class Word
    {
        [JsonProperty("word")]
        private string wordValue;
        [JsonProperty("category")]
        private string category;
        [JsonProperty("description")]
        private string description;
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        private string pathToImage = "./Resource/NoImage.png";

        [JsonIgnore]
        public string WordValue { get => wordValue; set => wordValue = value; }
        [JsonIgnore]
        public string Category { get => category; set => category = value; }
        [JsonIgnore]
        public string Description { get => description; set => description = value; }
        [JsonIgnore]
        public string PathToImage { get => pathToImage; set => pathToImage = value; }

        public Word()
        {
            /*EMPTY FOR JSON DESERIALIZER*/
        }

        public Word(string wordValue, string category, string description, string pathToImage)
        {
            WordValue = wordValue;
            Category = category;
            Description = description;
            PathToImage = pathToImage;
        }

        public Word(string wordValue, string category, string description)
        {
            WordValue = wordValue;
            Category = category;
            Description = description;
            PathToImage = null;
        }

    }
}