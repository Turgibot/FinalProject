#pip install --upgrade imutils
#pip install --upgrade scipy

# import the necessary packages
from scipy.spatial import distance as dist
from imutils.video import VideoStream
from imutils import face_utils
from threading import Thread
import numpy as np
import playsound
import argparse
import imutils
import time
import dlib
import cv2

def sound_alarm(path):
	# play an alarm sound
	playsound.playsound(path)

def eye_aspect_ratio(eye):
	# compute the euclidean distances between the two sets of
	# vertical eye landmarks (x, y)-coordinates
	A = dist.euclidean(eye[1], eye[5])
	B = dist.euclidean(eye[2], eye[4])

	# compute the euclidean distance between the horizontal
	# eye landmark (x, y)-coordinates
	C = dist.euclidean(eye[0], eye[3])

	# compute the eye aspect ratio
	ear = (A + B) / (2.0 * C)

	# return the eye aspect ratio
	return ear

# construct the argument parse and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-p", "--shape-predictor", required=True,
	help="path to facial landmark predictor")
ap.add_argument("-a", "--alarm", type=str, default="",
	help="path alarm .WAV file")
ap.add_argument("-w", "--webcam", type=int, default=0,
	help="index of webcam on system")
args = vars(ap.parse_args())
 
# define two constants, one for the eye aspect ratio to indicate
# blink and then a second constant for the number of consecutive
# frames the eye must be below the threshold for to set off the
# alarm
EYE_AR_THRESH = 0.3
EYE_AR_CONSEC_FRAMES = 48

# initialize the frame counter as well as a boolean used to
# indicate if the alarm is going off
COUNTER = 0
ALARM_ON = False

# initialize dlib's face detector (HOG-based) and then create
# the facial landmark predictor
print("[INFO] loading facial landmark predictor...")
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor(args["shape_predictor"])

# grab the indexes of the facial landmarks for the left and
# right eye, respectively
(lStart, lEnd) = face_utils.FACIAL_LANDMARKS_IDXS["left_eye"]
(rStart, rEnd) = face_utils.FACIAL_LANDMARKS_IDXS["right_eye"]

# start the video stream thread
print("[INFO] starting video stream thread...")
vs = VideoStream(src=args["webcam"]).start()
time.sleep(1.0)

# loop over frames from the video stream
while True:
	# grab the frame from the threaded video file stream, resize
	# it, and convert it to grayscale
	# channels)
	frame = vs.read()
	frame = imutils.resize(frame, width=450)
	gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

	# detect faces in the grayscale frame
	rects = detector(gray, 0)

	# loop over the face detections
	for rect in rects:
		# determine the facial landmarks for the face region, then
		# convert the facial landmark (x, y)-coordinates to a NumPy
		# array
		shape = predictor(gray, rect)
		shape = face_utils.shape_to_np(shape)

		# extract the left and right eye coordinates, then use the
		# coordinates to compute the eye aspect ratio for both eyes
		leftEye = shape[lStart:lEnd]
		rightEye = shape[rStart:rEnd]
		leftEAR = eye_aspect_ratio(leftEye)
		rightEAR = eye_aspect_ratio(rightEye)

		# average the eye aspect ratio together for both eyes
		ear = (leftEAR + rightEAR) / 2.0

		# compute the convex hull for the left and right eye, then
		# visualize each of the eyes
		leftEyeHull = cv2.convexHull(leftEye)
		rightEyeHull = cv2.convexHull(rightEye)
		cv2.drawContours(frame, [leftEyeHull], -1, (0, 255, 0), 1)
		cv2.drawContours(frame, [rightEyeHull], -1, (0, 255, 0), 1)

		# check to see if the eye aspect ratio is below the blink
		# threshold, and if so, increment the blink frame counter
		if ear < EYE_AR_THRESH:
			COUNTER += 1

			# if the eyes were closed for a sufficient number of
			# then sound the alarm
			if COUNTER >= EYE_AR_CONSEC_FRAMES:
				# if the alarm is not on, turn it on
				if not ALARM_ON:
					ALARM_ON = True

					# check to see if an alarm file was supplied,
					# and if so, start a thread to have the alarm
					# sound played in the background
					if args["alarm"] != "":
						t = Thread(target=sound_alarm,
							args=(args["alarm"],))
						t.deamon = True
						t.start()

				# draw an alarm on the frame
				cv2.putText(frame, "DROWSINESS ALERT!", (10, 30),
					cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)

		# otherwise, the eye aspect ratio is not below the blink
		# threshold, so reset the counter and alarm
		else:
			COUNTER = 0
			ALARM_ON = False

		# draw the computed eye aspect ratio on the frame to help
		# with debugging and setting the correct eye aspect ratio
		# thresholds and frame counters
		cv2.putText(frame, "EAR: {:.2f}".format(ear), (300, 30),
			cv2.FONT_HERSHEY_SIMPLEX, 0.7, (0, 0, 255), 2)

	# show the frame
	cv2.imshow("Frame", frame)
	key = cv2.waitKey(1) & 0xFF

	# if the `q` key was pressed, break from the loop
	if key == ord("q"):
		break

# do a bit of cleanup
cv2.destroyAllWindows()
vs.stop()



#url = 'http://192.168.1.100:8080/snapshot?topic=/camera/color/image_raw'
#server_url ='http://localhost:53983/api/DetectPeoples'
## construct the argument parse and parse the arguments
#ap = argparse.ArgumentParser()
##ap.add_argument("-i", "--image", required=True,
##                help="path to input image")
#ap.add_argument("-p", "--prototxt", required=True,
#                help="path to Caffe 'deploy' prototxt file")
#ap.add_argument("-m", "--model", required=True,
#                help="path to Caffe pre-trained model")
#ap.add_argument("-c", "--confidence", type=float, default=0.2,
#                help="minimum probability to filter weak detections")
#args = vars(ap.parse_args())


## initialize the list of class labels MobileNet SSD was trained to
## detect, then generate a set of bounding box colors for each class
#CLASSES = ["background", "aeroplane", "bicycle", "bird", "boat", "bottle", "bus", "car", "cat", "chair", "cow",
#           "diningtable", "dog", "horse", "motorbike", "person", "pottedplant", "sheep", "sofa", "train", "tvmonitor"]

#COLORS = np.random.uniform(0, 255, size=(len(CLASSES), 3))
## load our serialized model from disk
#print("[INFO] loading model...")
#net = cv2.dnn.readNetFromCaffe(args["prototxt"], args["model"])

## load the input image and construct an input blob for the image
## by resizing to a fixed 300x300 pixels and then normalizing it
## (note: normalization is done via the authors of the MobileNet SSD
## implementation)
#while True:
#    #save and count all detections
#    person_count = 0
#    boxes = ""
#    #TODO check for responce success
#    #get a single image at a time
#    resp = urlreq.urlopen(url)
#    image = np.asarray(bytearray(resp.read()), dtype='uint8')
#    image = cv2.imdecode(image, cv2.IMREAD_COLOR)
    
#    original_shape = image.shape
#    (h, w) = image.shape[:2]
#    blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 0.007843, (300, 300), 127.5)

#    # pass the blob through the network and obtain the detections and
#    # predictions

#    net.setInput(blob)
#    detections = net.forward()

#    # loop over the detections
#    for i in np.arange(0, detections.shape[2]):
#        # extract the confidence (i.e., probability) associated with the
#        # prediction
#        confidence = detections[0, 0, i, 2]

#        # filter out weak detections by ensuring the `confidence` is
#        # greater than the minimum confidence
#        if confidence > args["confidence"]:
#            # extract the index of the class label from the `detections`,
#            # then compute the (x, y)-coordinates of the bounding box for
#            # the object
#            idx = int(detections[0, 0, i, 1])
#            if CLASSES[idx] is not 'person':
#                continue
            
#            box = detections[0, 0, i, 3:7] * np.array([w, h, w, h])
#            (startX, startY, endX, endY) = box.astype("int")
#            box_str = '{}:{}:{}:{}'.format(startX, startY, endX, endY)
#            # display the prediction
#            # label = "{}: {:.2f}%".format(CLASSES[idx], confidence * 100)
#            # print("[INFO] {}".format(label))
#            accuracy = '{:.2f}'.format(confidence * 100)
            

#            #draw rectangle for debugging

#            #cv2.rectangle(image, (startX, startY), (endX, endY),
#            #              COLORS[idx], 2)
#            #y = startY - 15 if startY - 15 > 15 else startY + 15
#            #cv2.putText(image, label, (startX, y),
#            #            cv2.FONT_HERSHEY_SIMPLEX, 0.5, COLORS[idx], 2)

#            person_count += 1
#            boxes += accuracy +':'+ box_str + '%%%'
#    #send a post request of detections and empty list
   
#    r = requests.post(server_url, data = {"NumberOfPeople":person_count,"ValuesString":boxes})


#    #show the output image for debugging

#    #cv2.resize(image, original_shape[:2])
#    #cv2.imshow("Output", image)
#    #if cv2.waitKey(1) & 0xFF == ord('q'):
#    #    break
#cv2.destroyAllWindows()


