import datetime
import sys
import threading
from PIL import Image
import requests
from io import BytesIO
import cv2
import numpy as np


CLASSES = ["background", "aeroplane", "bicycle", "bird", "boat",
	"bottle", "bus", "car", "cat", "chair", "cow", "diningtable",
	"dog", "horse", "motorbike", "person", "pottedplant", "sheep",
	"sofa", "train", "tvmonitor"]
COLORS = np.random.uniform(0, 255, size=(len(CLASSES), 3))


class EuclidCamera(object):
    def __init__(self,  source, target, model, weights, host, confidence):
        self.video = cv2.VideoCapture(source)
        self.confidence = confidence
        self.net = cv2.dnn.readNetFromCaffe(weights, model)
        self.inferece_thread = threading.Thread(target=self.get_inference)
        self.img_ready = False
        self.image = []
        self.current_detections = {}
        self.current_h = 0
        self.current_w = 0
        self.detections = {}
        self.h = 0
        self.w = 0
        self.inference_started = False
        self.person_counter = 0;
        self.host=host

    def __del__(self):
        self.video.release()

    def get_frame(self):
        success, self.image = self.video.read()
        if self.img_ready is True:
            # loop over the detections
            self.detections = self.current_detections
            self.h = self.current_h
            self.w = self.current_w

        if self.inference_started is True and len(self.detections):
            counter = 0
            for i in np.arange(0, self.detections.shape[2]):
                confidence = self.detections[0, 0, i, 2]
                if confidence > self.confidence:

                    # extract the index of the class label from the `detections`,
                    # then compute the (x, y)-coordinates of the bounding box for
                    # the object
                    idx = int(self.detections[0, 0, i, 1])
                    if idx is 15:
                        counter += 1
                        box = self.detections[0, 0, i, 3:7] * np.array([self.w, self.h, self.w, self.h])
                        (startX, startY, endX, endY) = box.astype("int")

                        # display the prediction
                        label = "{}: {:.2f}%".format(CLASSES[idx], confidence * 100)
                        # print("[INFO] {}".format(label))
                        cv2.rectangle(self.image, (startX, startY), (endX, endY),
                                        COLORS[idx], 2)
                        y = startY - 15 if startY - 15 > 15 else startY + 15
                        cv2.putText(self.image, label, (startX, y),
                                    cv2.FONT_HERSHEY_SIMPLEX, 0.5, COLORS[idx], 2)
            if counter is not self.person_counter:
                requests.put('http://localhost:53983/api/MainViewerAPI/1', data={'InferenceResult': counter,'MaxPeopleAllowed': 0});
                self.person_counter = counter;

            self.img_ready = False


        # We are using Motion JPEG, but OpenCV defaults to capture raw images,
        # so we must encode it into JPEG in order to correctly display the
        # video stream.
        ret, jpeg = cv2.imencode('.jpg', self.image)
        return jpeg.tobytes()


    def get_inference(self):
        self.inference_started = True
        while True:
            if self.img_ready is False:

                (self.current_h, self.current_w) = self.image.shape[:2]
                blob = cv2.dnn.blobFromImage(cv2.resize(self.image, (300, 300)), 0.007843, (300, 300), 127.5)
                # pass the blob through the network and obtain the detections and
                # predictions

                self.net.setInput(blob)
                self.current_detections = self.net.forward()
                self.img_ready = True

