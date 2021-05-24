from random import random
import numpy as np

class NonUnif:

    def __init__(self, *params):
        self.X, self.p = np.split(np.array(params), 2)

    def simulate(self):
        k = 0
        F = self.p[0]
        u = random()
        while u >= F:
            k = k + 1
            F = F + self.p[k]
        return int(self.X[k])
