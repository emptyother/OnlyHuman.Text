using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace OnlyHuman
{
	[Cmdlet(VerbsCommon.Join, "Lines")]
	[OutputType(typeof(string[]))]
	public class JoinLinesCmdletCommand : PSCmdlet
	{
		[Parameter(
			Mandatory = true,
			Position = 0,
			ValueFromPipeline = true
		)]
		public string InputText { get; set; }
		private List<string> strcol;

		// This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
		protected override void BeginProcessing()
		{
			WriteVerbose("Begin!");
			WriteObject("Starting...");
			strcol = new List<string>();
		}

		// This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
		protected override void ProcessRecord()
		{
			if (string.IsNullOrWhiteSpace(InputText))
			{
				WriteObject("Paragraph:");
				var fullstring = string.Join(" ", strcol);
				WriteObject(fullstring);
				strcol.Clear();
				WriteObject("BLANK");
				WriteObject(InputText);
			}
			else
			{
				strcol.Add(InputText.Trim());
			}
		}

		// This method will be called once at the end of pipeline execution; if no input is received, this method is not called
		protected override void EndProcessing()
		{
			WriteObject("Ending...");
			WriteVerbose("End!");
		}
	}
}
