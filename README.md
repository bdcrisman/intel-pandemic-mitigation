# image-reel #

## Short Description
* Side by side comparison with images responding to delta.

## Long Description
* Side by side comparison with images responding to delta.
* Can run a dual comparison demo or a single data output demo.
* Utilizes SSH to start workloads and request data.
* Can use video or static images.
* Video will set playback speed to incoming performance speeds, based on the max of the meter.
    * E.g. if the meter max is 100 and the incoming value is 90, then the video will play at 90% that of the original playback speed.

## Requirements
* Windows 10
* Image formats supported: [ .png, .jpg ]
* Video formats supported: [ .mp4 ] 

## Usage
* **All configuration files and backup data are located in ***```/PerformanceComparisonTemplate_Data/StreamingAssets/```***
1. Edit ```/config/demoConfig.json``` to set the demo title among a few other details.
2. Edit ```/config/serverConfig.json``` to set specific server/workload details, meter colors, flags, and backup data locations.
3. Edit ```/backup/future.json``` and ```/backup/present.json``` files with appropriate values and offset percentages.
4. Place videos/images in the appropriate ```/background/```, ```/future/```, or ```/present/``` folders in ```/media/```. If more than one video is in the same folder, only the first video will be used in the demo.
5. Run the executable, set your resolution.
6. Click or press on the ***Start*** button to begin the demo.

## Configs
### **```demoConfig.json```**
- ```demoTitle``` : Main demo title
- ```demoSubtitle``` : Demo subtitle
- ```performanceSubtitle``` : Subtitle to display below performance value.
- ```performanceUnits``` : Performance units. Generally will be "X".

### **```serverConfig.json```**
* ***The file displays and array of servers. The top most server will be displayed on the left-hand-side of the screen.***
* ***Only supports 1-2 servers.***
* ***Top-most server is displayed on the left, the bottom server on the right.***
* ***If only one server, only one meter will be shown, in the middle.***
- ```title``` : Title to display for the server/product.
- **```ssh```** : SSH credentials and commands
    - ```ip``` : IP address
    - ```user``` : User account
    - ```password``` : Password of user
    - ```workloadCmd``` : Command to run workload.
        - i.e. *```"/home/demo/example/run_demo.sh"```*
    - ```logCmd``` : Command to get data.
        - i.e. *```"tail -n 1 /home/demo/example/demo.log"```*
    - ```timeoutMS``` : Timeout in milliseconds to allow for SSH connection.
- **```meter```** : Meter details
    - ```max``` : Meter's max value.
    - ```units``` : Units to display.
    - ```borderColorHex``` : Color of the meter border in hex format.
    - ```fillColorHex``` : Color of the fill portion of the meter in hex format.
    - ```style``` : Style of meter to use. 
        - Currently supports [```"bar"```, ```"circle"```]
            - e.g. ```"style": "bar"```
- **```flags```** : Fine tuning flags to be set for particular workloads and data collection.
    - ```isFutureGen``` : *```true/false```* Is this the future gen product being promoted?
    - ```runBackup``` : *```true/false```* Run this server/product demo from backup?
    - ```displayRawData``` : *```true/false```* Can we display the raw data?
    - ```loopWorkload``` : *```true/false```* Do we need to loop the workload? *false will run the workload once.*
    - ```stdoutData``` : *```true/false```* Is the data delivered via stdout? *false will read a log file instead of stdout.*
- **```delays```** : Individual delays
    - ```loopWorkloadDelayMS``` : Delay in milliseconds to wait before running the workload command.
    - ```loopDataRequestDataMS``` : Delay in milliseconds to wait before requesting and displaying data.

## Backup
* ***```future.json```*** and ***```present.json```*** are files that contain backup data and are the same format.
- ```value``` : Main value gathered during previous runs.
- ```offsetPercentage``` : Offset of *```value```* to display.
    - i.e. an *```offsetPercentage```* of 2 will display a random number in range *```value```* +- 2%*

## Media
* ***Videos and images. Only place one image or video in directories as first video or image in directory is chosen.***
- **```/images/```**
    - ```/background/``` : Folder that contains the demo background.
    - ```/future/``` : Folder that contains static image to display for the future generation server/product.
    - ```/present/``` : Folder that contains static image to display for the present generation server/product.
- **```/videos/```**
    - ```/future/``` : Folder that contains video for the future generation server/product.
    - ```/present/``` : Folder that contains video for the present generation server/product.
