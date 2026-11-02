# Repetier Server

## printer/list

```json
{
	"data": [
		{"name":"Prusa i3 MK3S_0,4","slug":"Prusa_i3_MK3S_2","online":1,"jobFlags":127,"serverTime":1793598639.966,"job":"none","jobid":0,"jobstate":"","paused":false,"pauseState":0,"active":true},
		{"name":"Prusa i3 MK3S_0,6","slug":"Prusa_i3_MK3S","online":1,"jobFlags":127,"serverTime":1793598639.966,"job":"none","jobid":0,"jobstate":"","paused":false,"pauseState":0,"active":true}
	]
}
```

## printer/api/{printerSlug}/updateAvailable

```json
{
	"message":"",
	"messageHtml":"",
	"versionMessage":"## New Features...",
	"versionName":"2.0.1",
	"betaActive":false,
	"features":4095,
	"updateAvailable":false,
	"ignoreVersion":"0.0.0",
	"showUpdate":false,
	"downloadUrl":"https://github.com/repetier/Repetier-Server",
	"free":true,
	"branded":false,
	"licensed":true,
	"rentedExpired":0,
	"installerType":1,
	"currentVersion":"2.0.1",
	"demo":false,
	"printerFrontendUrl":"/modules/front2/www/app.html",
	"webFrontendUrl":"/",
	"teaser":{
		"available":true,
		"end":1605431065000,
		"msg":"Get your PRO license for only 59.99 € and save 20,- € now!",
		"start":1479200665000,
		"updated":629984218,
		"url":"https://www.repetier.com/qr/s.php?s=buyserver"
		},
	"testperiodMode":1,
	"licensingDisabled":true,
	"informerAvailable":true,
	"informerNotice":false,
	"informerEndDate":"2027-06-30",
	"updateCheckRetired":false,
	"updateCheckUrl":"https://github.com/repetier/Repetier-Server",
	"updateCheckEndDate":"2027-06-30",
	"currentBrandingVersion":0,
	"availableBrandingVersion":0
}
```

## printer/api/{printerSlug}/listExternalCommands

```json
[
	{
	"id":0,
	"name":"Shutdown Server",
	"confirm":"Really shut down the server?",
	"slug":"",
	"icon":"",
	"local":true,
	"remote":true,
	"permPrint":true,
	"permConfig":true,
	"permAdd":true,
	"permDel":false,
	"terminal":false,
	"execute":"sudo /sbin/shutdown -h now",
	"ifThisNotPrinting":false,
	"ifAllNotPrinting":false
	},
	{
	"id":1,
	"name":"Reboot Server",
	"confirm":"Really reboot the server?",
	"slug":"",
	"icon":"",
	"local":true,
	"remote":true,
	"permPrint":true,
	"permConfig":true,
	"permAdd":true,
	"permDel":false,
	"terminal":false,
	"execute":"sudo /sbin/shutdown -r now",
	"ifThisNotPrinting":false,
	"ifAllNotPrinting":false
	}]
```

## printer/api/{printerSlug}/GPIOGetList

```json
{
	"list":[{
		"uuid":"77770ca6-96ba-46a5-888c-b1a2ec0ad0b6",
		"name":"TestGPIO",
		"slug":"",
		"display":"GPIO Test",
		"securityQuestion":false,
		"icon":"",
		"iconOff":"",
		"description":"Test für GPIO Abfrage",
		"error":"",
		"parameter":"",
		"debounceMS":2,
		"pinNumber":1,
		"chip":0,
		"showInMenu":false,
		"pwmFrequency":1000.0,
		"pwmDutyCycle":0.0,
		"initEnabled":false,
		"pwmInitDutyCycle":0.0,
		"pwmPolarity":false,
		"state":false,
		"pos":0,
		"operation":3,
		"direction":1,
		"edge":0,
		"bias":1,
		"drive":2
		}],
	"ok":true
}
```

## printer/api/{printerSlug}/historyList

```json
{
  "list": [
    {
      "id": 2130,
      "printerName": "Prusa i3 MK3S_0,4",
      "printerUUID": "",
      "printerSlug": "Prusa_i3_MK3S_2",
      "filename": "Trennwand_0.6n_0.3mm_PETG_MK3S_1h2m",
      "username": "q2917",
      "startTime": 1791532282.979,
      "endTime": 1791536353.88,
      "status": 1,
      "filament": 10658.630029,
      "notes": "",
      "computedTime": 4239.206565,
      "year": 2026,
      "month": 10,
      "report": "20261009T105913_Trennwand_0.6n_0.3mm_PETG_MK3S_1h2m.pdf",
      "pauseTime": 0,
      "costs": 2.7203
    },
    {
      "id": 2129,
      "printerName": "Prusa i3 MK3S_0,4",
      "printerUUID": "",
      "printerSlug": "Prusa_i3_MK3S_2",
      "filename": "My_awesome_file_0.2mm_PETG_MK3_3h7m",
      "username": "frontend",
      "startTime": 1790228470.068,
      "endTime": 1790239645.047,
      "status": 1,
      "filament": 13732.168174,
      "notes": "",
      "computedTime": 11343.899647,
      "year": 2026,
      "month": 9,
      "report": "20260924T104725_My_awesome_file_0.2mm_PETG_MK3_3h7m.pdf",
      "pauseTime": 0,
      "costs": 5.152004
    }
  ],
  "total": 760
}
```

```json

```