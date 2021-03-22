using System;
using System.IO;
using System.Linq;

namespace IBM.Watson.TextToSpeech.v1.Websockets
{
    public static class WaveUtils
    {
        /** The WAVE meta-data header size. (value is 8) */
        private static readonly int WAVE_HEADER_SIZE = 8;

        /** The WAVE meta-data position in bytes. (value is 74) */
        private static readonly int WAVE_METADATA_POS = 74;

        /** The WAVE meta-data size position. (value is 4) */
        private static readonly int WAVE_SIZE_POS = 4;

        /**
		* Writes an number into an array using 4 bytes.
		*
		* @param value the number to write
	    * @param array the byte array
	    * @param offset the offset
	    */
        private static void WriteInt(int value, Stream audioStream, int offset)
        {
            audioStream.Position = offset;
            for (int i = 0; i < 4; i++)
            {
                audioStream.WriteByte((byte)((uint)value >> (8 * i)));
            }
            audioStream.Position = 0;
        }
        /**
		* Re-writes the data size in the header(bytes 4-8) of the WAVE(.wav) input stream.<br>
		* It needs to be read in order to calculate the size.
		*
		* @param is the input stream
		* @return A new input stream that includes the data header in the header
		* @throws IOException Signals that an I/O exception has occurred.
		*/
        public static void ReWriteWaveHeader(Stream audioStream)
        {
            byte[] audioBytes = ((MemoryStream)audioStream).ToArray();
            int filesize = audioBytes.Length - WAVE_HEADER_SIZE;

            WriteInt(filesize, audioStream, WAVE_SIZE_POS);

            // the first subchunk is at byte 12, the fmt subchunk
            // this is the only other reliable constant
            var chunkIdOffset = 12;
            const int fieldSize = 4;

            // every subchunk has a 4 byte id followed by a 4 byte size field
            var chunkSizeOffset = chunkIdOffset + fieldSize;
            var subchunk2sizeLocation = 0;

            // initialize values to hold data of each chunk we come across
            var tempChunkID = "";
            var tempChunkSize = 0;

            while (tempChunkID != "data")
            {
                if (chunkSizeOffset + fieldSize > audioBytes.Length)
                {
                    break;
                }

                tempChunkID = System.Text.Encoding.ASCII.GetString(audioBytes.Skip(chunkIdOffset)
                  .Take(fieldSize).ToArray());

                String decoded = System.Text.Encoding.ASCII.GetString(audioBytes.Skip(chunkSizeOffset).Take(4).ToArray()).Trim();
                tempChunkSize = BitConverter.ToInt32(audioBytes, chunkSizeOffset);

                // save the location of the data size field
                if (tempChunkID == "data")
                {
                    subchunk2sizeLocation = chunkSizeOffset;
                }

                // skip over all the data in the temp chunk
                chunkIdOffset = chunkSizeOffset + fieldSize + tempChunkSize;
                chunkSizeOffset = chunkIdOffset + fieldSize;
            }

            int subchunk2size = audioBytes.Length - subchunk2sizeLocation - fieldSize;

            WriteInt(subchunk2size, audioStream, subchunk2sizeLocation);
        }
    }
}
