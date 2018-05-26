import datetime
import sys
#print(datetime.datetime.now())
import threading
from PIL import Image
import requests
from io import BytesIO
import cv2

print "start"
time = datetime.datetime.now()
source = "http://192.168.1.10:8088/stream?topic=/camera/color/image_raw"
target = "D:\Projects\FinalProject\CSA_Project\CSA_Project\Euclid"
threads = []


def get_image():

    cap = cv2.VideoCapture(source)

    while True:
        # Capture frame-by-frame
        ret, frame = cap.read()
        sys.stdout.write(frame.tostring())
        # Our operations on the frame come here


        # Display the resulting frame
        cv2.imshow('frame', frame)
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    # When everything done, release the capture
    cap.release()
    cv2.destroyAllWindows()


print datetime.datetime.now() - time


t1 = threading.Thread(target=get_image)
t1.start()
print "stop"
