namespace h8s
{
    public enum NodeKind { CreateVPC = 0, CreateSubnet = 1 }

    public enum NodeAutomoton { Terraform, Ansible }

    public enum DataType { Exec, Bool, String, Object }

    public enum PortDirection { Ingress, Egress }
}
