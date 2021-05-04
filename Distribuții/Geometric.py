from random import random
from math import log as ln

class Geom:

    def __init__(self, p):
        self.p = p

    def simulate(self):
        u = random()
        return int(ln(1 - u)/ln(1 - self.p)) + 1
