using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace settleMetal
{
    class ShadowEffect : RoutingEffect
    {
        public ShadowEffect(string effectId) : base(effectId)
        {
        }

        public float Radius { get; set; }
    
        public Color Color { get; set; }

        public float DistanceX { get; set; }

        public float DistanceY { get; set; }
    }
}
