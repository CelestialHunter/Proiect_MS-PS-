from random import random
import numpy as np

class NonUnif:

    def __init__(self, a, b, *p):
        self.a = int(a)
        self.b = int(b)
        self.p = np.array(p)
        self.n = b - a
        self.interval = self.generate_interval()

    def generate_interval(self):
        return np.linspace(self.a, self. b, self.b - self.a + 1, dtype=int)

    def simulate(self):
        k = 0
        F = self.p[0]
        u = random()
        while u >= F:
            k = k + 1
            F = F + self.p[k]
        return self.interval[k]
