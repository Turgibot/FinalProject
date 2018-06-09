import datetime
import sys
#print(datetime.datetime.now())
import threading
from PIL import Image
import requests
from io import BytesIO
import cv2
import numpy as np

# source = "http://192.168.1.101:8088/stream?topic=/camera/color/image_raw"
# target = "D:\Projects\FinalProject\CSA_Project\CSA_Project\Euclid"

CLASSES = ["background", "aeroplane", "bicycle", "bird", "boat",
	"bottle", "bus", "car", "cat", "chair", "cow", "diningtable",
	"dog", "horse", "motorbike", "person", "pottedplant", "sheep",
	"sofa", "train", "tvmonitor"]
COLORS = np.random.uniform(0, 255, size=(len(CLASSES), 3))


class EuclidCamera(object , source, target, model, weights, confidence):
    def __init__(self):
        self.video = cv2.VideoCapture(source)
        self.net = cv2.dnn.readNetFromCaffe(weights, model)
    def __del__(self):
        self.video.release()

    def get_frame(self):
        success, image = self.video.read()
        #(h, w) = image.shape[:2]
        #blob = cv2.dnn.blobFromImage(cv2.resize(image, (300, 300)), 0.007843, (300, 300), 127.5)
        ## pass the blob through the network and obtain the detections and
        ## predictions
        #print("[INFO] computing object detections...")
        #self.net.setInput(blob)
        #detections = self.net.forward()

        ## loop over the detections
        #for i in np.arange(0, detections.shape[2]):
        #    # extract the index of the class label from the `detections`,
        #    # then compute the (x, y)-coordinates of the bounding box for
        #    # the object
        #    idx = int(detections[0, 0, i, 1])
        #    box = detections[0, 0, i, 3:7] * np.array([w, h, w, h])
        #    (startX, startY, endX, endY) = box.astype("int")

        #    # display the prediction
        #    label = "{}: {:.2f}%".format(CLASSES[idx], confidence * 100)
        #    # print("[INFO] {}".format(label))
        #    cv2.rectangle(image, (startX, startY), (endX, endY),
        #                  COLORS[idx], 2)
        #    y = startY - 15 if startY - 15 > 15 else startY + 15
        #    cv2.putText(image, label, (startX, y),
        #                cv2.FONT_HERSHEY_SIMPLEX, 0.5, COLORS[idx], 2)

        ## We are using Motion JPEG, but OpenCV defaults to capture raw images,
        ## so we must encode it into JPEG in order to correctly display the
        ## video stream.
        ret, jpeg = cv2.imencode('.jpg', image)
        return jpeg.tobytes()
