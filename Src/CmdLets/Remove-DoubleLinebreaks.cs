using System.Management.Automation;

namespace OnlyHuman.Text
{
	[Cmdlet(VerbsCommon.Remove, "DoubleLineBreaks")]
	[OutputType(typeof(string))]
	public class Remove_DoubleLinebreaks : PSCmdlet
	{
		[Parameter(
			Position = 0,
			ValueFromPipeline = true
		)]
		public string[] InputText { get; set; }

		private bool PreviousLineWasWhitespace = false;

		protected override void BeginProcessing()
		{
			PreviousLineWasWhitespace = false;
		}

		protected override void ProcessRecord()
		{
			foreach (var line in InputText)
			{
				if (!string.IsNullOrWhiteSpace(line))
				{
					PreviousLineWasWhitespace = false;
					WriteObject(line);
					continue;
				}
				// If currentline is a whitespace..
				if(PreviousLineWasWhitespace) {
					// Dont output whitespaced lines if previous line was whitespace.
					continue;
				}
				// If the current line is a whitespace, and previous line wasn't.
				WriteObject(line);
				PreviousLineWasWhitespace = true;
			}
		}

		protected override void EndProcessing()
		{
		}
	}
}
