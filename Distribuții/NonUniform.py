from random import random
import numpy as np

class NonUnif:

    def __init__(self, *valori):
        self.n = int(len(valori)/2)
        self.interval = np.array(valori[:self.n], dtype=int)
        self.p = np.array(valori[self.n:])

    def simulate(self):
        k = 0
        F = self.p[0]
        u = random()
        while u >= F:
            k = k + 1
            F = F + self.p[k]
        return self.interval[k]
