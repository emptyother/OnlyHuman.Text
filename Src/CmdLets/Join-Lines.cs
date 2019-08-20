using System.Collections.Generic;
using System.Management.Automation;

namespace OnlyHuman.Text
{
	/// <summary>
	/// Join lines to create paragraphs. If two lines don't have an empty line between them,
	/// consider them a paragraph and join them. If there is a line-break between them, 
	/// create a new paragraph.
	/// </summary>
	[Cmdlet(VerbsCommon.Join, "Lines")]
	[OutputType(typeof(string))]
	public class Join_Lines : PSCmdlet
	{
		/// <summary>
		/// An array of text that should be turned into paragraphs.
		/// </summary>
		/// <value></value>
		[Parameter(
			Position = 0,
			ValueFromPipeline = true
		)]
		public string[] InputText { get; set; }

		private List<string> textBuffer;

		protected override void BeginProcessing()
		{
			textBuffer = new List<string>();
		}

		protected override void ProcessRecord()
		{
			foreach (var line in InputText)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					FlushTextBuffer();

					// If it contains whitespace, output those.
					if(!string.IsNullOrEmpty(line)) {
						WriteObject(line);
					} else {
						// If not, just output a blank line.
						WriteObject("");
					}
				}
				else
				{
					textBuffer.Add(line.Trim());
				}
			}
		}

		private void FlushTextBuffer()
		{
			if (textBuffer.Count > 0)
			{
				WriteVerbose("Paragraph:");
				WriteObject(string.Join(" ", textBuffer));
				textBuffer.Clear();
			}
		}

		protected override void EndProcessing()
		{
			FlushTextBuffer();
		}
	}
}
