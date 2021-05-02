from random import random

class Unif:

    def __init__(self, a, b):
        self.a = a
        self.b = b

    def simulate(self):
        n = self.b - self.a + 1
        u = random()
        x = int(n * u)
        return x + int(self.a)
