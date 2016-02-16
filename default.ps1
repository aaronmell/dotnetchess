properties {
    $nunitConsolePath = '.\packages\NUnit.Console.3.0.1\tools\nunit3-console.exe'
}


task default -depends Clean, Build, Test 

task Clean {
	exec { msbuild dotnetengine.sln '/t:Clean' /nologo /verbosity:Minimal }
}

task Build {
	exec { msbuild dotnetengine.sln '/t:Build' /nologo /verbosity:Minimal  }
}

task Test {    
    & $nunitConsolePath 'dotnetchess.nunit' "--result:TestResult.xml;format=nunit2"
}