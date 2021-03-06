using Crash;
using System;

namespace CrashEdit
{
    public sealed class OldSEQController : Controller
    {
        private OldMusicEntryController oldmusicentrycontroller;
        private SEQ seq;

        public OldSEQController(OldMusicEntryController oldmusicentrycontroller,SEQ seq)
        {
            this.oldmusicentrycontroller = oldmusicentrycontroller;
            this.seq = seq;
            Node.Text = "SEQ";
            Node.ImageKey = "seq";
            Node.SelectedImageKey = "seq";
            AddMenu("Replace SEQ",Menu_Replace_SEQ);
            AddMenu("Delete SEQ",Menu_Delete_SEQ);
            AddMenuSeparator();
            AddMenu("Export SEQ",Menu_Export_SEQ);
            AddMenu("Export SEQ as MIDI",Menu_Export_SEQ_MIDI);
        }

        public OldMusicEntryController OldMusicEntryController
        {
            get { return oldmusicentrycontroller; }
        }

        public SEQ SEQ
        {
            get { return seq; }
        }

        private void Menu_Replace_SEQ()
        {
            int i = oldmusicentrycontroller.OldMusicEntry.SEP.SEQs.IndexOf(seq);
            byte[] data = FileUtil.OpenFile(FileFilters.SEQ,FileFilters.Any);
            if (data != null)
            {
                seq = SEQ.Load(data);
                oldmusicentrycontroller.OldMusicEntry.SEP.SEQs[i] = seq;
            }
        }

        private void Menu_Delete_SEQ()
        {
            oldmusicentrycontroller.OldMusicEntry.SEP.SEQs.Remove(seq);
            Dispose();
        }

        private void Menu_Export_SEQ()
        {
            byte[] data = seq.Save();
            FileUtil.SaveFile(data,FileFilters.SEQ,FileFilters.Any);
        }

        private void Menu_Export_SEQ_MIDI()
        {
            byte[] data = seq.ToMIDI();
            FileUtil.SaveFile(data,FileFilters.MIDI,FileFilters.Any);
        }
    }
}
