using System;
using Newtonsoft.Json;

namespace Quicker.Data.Common
{
        public class Item : IComparable<Item> {
      
        public Item( string text, object value, int? sortId = null, string group = null, bool? disabled = null ) {
            Text = text;
            Value = value;
            SortId = sortId;
            Group = group;
            Disabled = disabled;
        }

        
        [JsonProperty( "text", NullValueHandling = NullValueHandling.Ignore )]
        public string Text { get; }

       
        [JsonProperty( "value", NullValueHandling = NullValueHandling.Ignore )]
        public object Value { get; }

       
        [JsonProperty( "sortId", NullValueHandling = NullValueHandling.Ignore )]
        public int? SortId { get; }

         
        [JsonProperty( "group", NullValueHandling = NullValueHandling.Ignore )]
        public string Group { get; }

        
        [JsonProperty( "disabled", NullValueHandling = NullValueHandling.Ignore )]
        public bool? Disabled { get; }

        
        public int CompareTo( Item other ) {
            return string.Compare( Text, other.Text, StringComparison.CurrentCulture );
        }
    }
}
