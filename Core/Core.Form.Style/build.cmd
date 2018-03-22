@echo off
setlocal enabledelayedexpansion

resgen Index.restext AbyssalDataProcessor.Core.Form.Style.Index.resources /str:vb,AbyssalDataProcessor.Core.Form.Style,Index,Index.vb /publicClass

set tFiles=
for /r %%i In (*.xslt) DO set tFiles=!tFiles! /embed:%%i

al.exe /out:AbyssalDataProcessor.Core.Form.Style.dll /v:1.0.0.0 /title:"Core From Style" /t:lib /prod:"Abyssal Data Processor" /c:en-US /embed:AbyssalDataProcessor.Core.Form.Style.Index.resources %tFiles%

:end