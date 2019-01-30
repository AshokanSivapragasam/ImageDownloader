while(1) {
    Try {
        # prepare input data
        $sleepTimeInMinutes = 1;
        $numberOfMessages = 0;
        $time = (Get-Date) – (New-TimeSpan -Day 1)

        # do work
        $results = Get-WinEvent –FilterHashtable @{logname=’application’; level=2; starttime=$time; } -ErrorAction Stop  | Select-Object ProviderName, Id, Message -First 1

        # aligning the results
        $measurements = $results | measure
        $numberOfMessages = $measurements.Count

        # writing results
        $results

        Write-Verbose ("Total number of records #{0}" -f $numberOfMessages) -Verbose
    } Catch {
        # absorb exceptions
        Write-Verbose ("Total number of records #{0}" -f $numberOfMessages) -Verbose
    }

    # suspend for a few moments..
    start-sleep -Seconds ($sleepTimeInMinutes * 60)
}