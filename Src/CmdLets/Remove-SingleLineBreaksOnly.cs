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

		private List<string> textBuffer;

		protected override void BeginProcessing()
		{
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
				textBuffer.Add(line);
			}
		}

		protected override void EndProcessing()
		{
			FlushTextBufferIfMultipleLineBreaks();
		}

		private void FlushTextBufferIfMultipleLineBreaks()
		{
			if (textBuffer.Count > 1)
			{
				foreach (var line in textBuffer)
				{
					WriteObject(line);
				}
			}
			textBuffer.Clear();
		}
	}
}
