using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slowka
{
    class Word
    {
        public Word()
        {
            this.Polish = "";
            this.English = "";
        }
        public Word(String pl, String eng)
        {
            this.Polish = pl;
            this.English = eng;
        }
        private String Polish;
        public String getPolish()
        {
            return this.Polish;
        }

        public void setPolish(String value)
        {
            this.Polish = value;
        }
        private String English;

        public String getEnglish()
        {
            return this.English;
        }

        public void setEnglish(String value)
        {
            this.English = value;
        }
    }
}
