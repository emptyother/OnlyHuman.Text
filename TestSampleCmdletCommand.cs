using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace OnlyHuman
{
	/// <summary>
	/// Join lines to create paragraphs. If two lines don't have an empty line between them,
	/// consider them a paragraph and join them. If there is a line-break between them, 
	/// create a new paragraph.
	/// </summary>
	[Cmdlet(VerbsCommon.Join, "Lines")]
	[OutputType(typeof(string))]
	public class JoinLinesCmdletCommand : PSCmdlet
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

		/// <summary>
		/// Holds the text while building a paragraph.
		/// </summary>
		private List<string> textBuffer;

		/// <summary>
		/// This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
		/// </summary>
		protected override void BeginProcessing()
		{
			textBuffer = new List<string>();
		}

		/// <summary>
		/// This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called.
		/// </summary>
		protected override void ProcessRecord()
		{
			foreach (var text in InputText)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					FlushTextBuffer();

					// If it contains whitespace, output those.
					if(!string.IsNullOrEmpty(text)) {
						WriteObject(text);
					} else {
						// If not, just output a blank line.
						WriteObject("");
					}
				}
				else
				{
					textBuffer.Add(text.Trim());
				}
			}
		}

		/// <summary>
		/// Outputs all text collected so far.
		/// </summary>
		private void FlushTextBuffer()
		{
			if (textBuffer.Count > 0)
			{
				WriteVerbose("Paragraph:");
				WriteObject(string.Join(" ", textBuffer));
				textBuffer.Clear();
			}
		}

		/// <summary>
		/// This method will be called once at the end of pipeline execution; if no input is received, this method is not called
		/// </summary>
		protected override void EndProcessing()
		{
			FlushTextBuffer();
		}
	}
}
