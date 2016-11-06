from numpy import *
import matplotlib.pyplot as plt

data = []
label = 1
with open('smallMnist_train.csv','r') as f:
	i = 0
	for line in f:
		i+=1
		if i == 250:
			data = line.split(';')[0].split(',')
			data = [float(x) for x in data]
			label = line.split(';')[1]
			break
			
data = reshape(array(data),(28,28))

print(label)
plt.imshow(data)
plt.show()