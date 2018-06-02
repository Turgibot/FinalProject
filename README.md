# CLOSED SPACE ANALYSER
## Using Intel Euclid to analyse the environment

### Students : 
<ul>
  <li>Shimon Genish</li>
  <li>Guy Tordjman</li>
  <li>Ohad Agayof</li>
  <li>Liran ben tovim</li>
</ul>
    
<p>
  Submitted in fulfillment  Of the requirements of
Software Engineering advanced lab
Instructor: Dr. Reuven Cohen
</p>


## Instructions and requirements Requirements
### Hardware:

<ol>
  <li>Intel Euclid</li>
  <li>Windows server</li>
  <li>MS SQL Server</li>
</ol>

### Software Server side
<ol>
  <li>Tensorflow +1.4.0 installed on Euclid and on Server</li>
  <li>Protobuf copiler for your OS</li>
  <li>Tensorflow model library</li>
  <li>Visual Studio 2015 or later</li>
  <li>Python 2.7</li>
  <li>IronPyton</li>
  <li>ffmpeg</li>
  <li>MS SQL Studio</li>
  <li>TensorFlow 1.4 or later</li>
  <li>Neural Network - we used tiny yolo>
</ol>

#### Note: if compiling the tensorflow objectdetection does'nt work for you please from CSA_Project try:

protoc --python_out=. .\object_detection\protos\anchor_generator.proto .\object_detection\protos\argmax_matcher.proto .\object_detection\protos\bipartite_matcher.proto .\object_detection\protos\box_coder.proto .\object_detection\protos\box_predictor.proto .\object_detection\protos\eval.proto .\object_detection\protos\faster_rcnn.proto .\object_detection\protos\faster_rcnn_box_coder.proto .\object_detection\protos\grid_anchor_generator.proto .\object_detection\protos\hyperparams.proto .\object_detection\protos\image_resizer.proto .\object_detection\protos\input_reader.proto .\object_detection\protos\losses.proto .\object_detection\protos\matcher.proto .\object_detection\protos\mean_stddev_box_coder.proto .\object_detection\protos\model.proto .\object_detection\protos\optimizer.proto .\object_detection\protos\pipeline.proto .\object_detection\protos\post_processing.proto .\object_detection\protos\preprocessor.proto .\object_detection\protos\region_similarity_calculator.proto .\object_detection\protos\square_box_coder.proto .\object_detection\protos\ssd.proto .\object_detection\protos\ssd_anchor_generator.proto .\object_detection\protos\string_int_label_map.proto .\object_detection\protos\train.proto .\object_detection\protos\keypoint_box_coder.proto .\object_detection\protos\multiscale_anchor_generator.proto
 

We acknoledge and thankfull to the yolo team for making the 
[yolo network](https://pjreddie.com/darknet/yolo)
available to the world.


### Environment Preparation:
<p>
	After installing all the required software you should set your global variables in the Global class.
	On Euclid stop all OOBE services using provided scripts 

</p>

