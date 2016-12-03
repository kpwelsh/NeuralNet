import matplotlib.pyplot as plt
from numpy import *

def getMaxAbs(arr):
	m = 0
	for x in arr:
		if abs(x) > m:
			m = abs(x)
	return m
file = loadtxt('autoEncoded.txt')
fig, axes = plt.subplots(10,10,sharex=True,sharey=True)
for i in range(100):
	data = reshape(array(file[i,:])/getMaxAbs(file[i,:]),(28,28))
	ax = axes[i//10,i%10]
	ax.imshow(data, cmap='gray',aspect='auto')
	ax.set_xlim([0,28])
	ax.set_ylim([0,28])
	ax.get_xaxis().set_visible(False)
	ax.get_yaxis().set_visible(False)
	
fig.subplots_adjust(hspace=0,wspace=0)
plt.figure()
plt.imshow(reshape(array(loadtxt('replication.txt')[0,:]),(28,28)))
plt.title("This is the fake one!")
plt.figure()
plt.imshow(reshape(array(loadtxt('replication.txt')[1,:]),(28,28)))
plt.title("This is the real one!")
plt.show()
