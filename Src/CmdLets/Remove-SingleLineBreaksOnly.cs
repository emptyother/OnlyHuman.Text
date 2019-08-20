using System.Collections.Generic;
using System.Management.Automation;

namespace OnlyHuman.Text
{
	[Cmdlet(VerbsCommon.Remove, "SingleLineBreaksOnly")]
	[OutputType(typeof(string))]
	public class Remove_SingleLineBreaksOnly : PSCmdlet
	{
		[Parameter(
			Position = 0,
			ValueFromPipeline = true
		)]
		public string[] InputText { get; set; }

		private List<string> TextBuffer;

		protected override void BeginProcessing()
		{
			TextBuffer = new List<string>();
		}

		protected override void ProcessRecord()
		{
			foreach (var line in InputText)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					FlushTextBufferIfMultipleLineBreaks();
					WriteObject(line);
					continue;
				}
				TextBuffer.Add(line);
			}
		}

		protected override void EndProcessing()
		{
			FlushTextBufferIfMultipleLineBreaks();
		}

		private void FlushTextBufferIfMultipleLineBreaks()
		{
			if (TextBuffer.Count > 1)
			{
				foreach (var line in TextBuffer)
				{
					WriteObject(line);
				}
			}
			TextBuffer.Clear();
		}
	}
}
